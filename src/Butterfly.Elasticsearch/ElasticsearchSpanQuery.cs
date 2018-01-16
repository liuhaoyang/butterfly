using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage.Query;
using Nest;
using ISpanQuery = Butterfly.Storage.ISpanQuery;

namespace Butterfly.Elasticsearch
{
    public class ElasticsearchSpanQuery : ISpanQuery
    {
        private readonly ElasticClient _elasticClient;
        private readonly IIndexFactory _indexFactory;

        public ElasticsearchSpanQuery(IElasticClientFactory elasticClientFactory, IIndexFactory indexFactory)
        {
            _indexFactory = indexFactory;
            _elasticClient = elasticClientFactory.Create();
        }

        public Task<Span> GetSpan(string spanId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Trace> GetTrace(string traceId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PageResult<Trace>> GetTraces(TraceQuery traceQuery)
        {
            var index = Indices.Parse(_indexFactory.CreateIndex());

            var query = BuildTracesQuery(traceQuery);

            var request = new SearchRequest(index)
            {
                From = (traceQuery.CurrentPageNumber - 1) * traceQuery.PageSize,
                Size = traceQuery.PageSize,
                Query = query
            };

            var searchQuery = _elasticClient.SearchAsync<Span>(request);

            var countRequest = new CountRequest(index)
            {
                Query = query
            };

            var totalMemberCountQuery = _elasticClient.CountAsync<Span>(countRequest);

            await Task.WhenAll(searchQuery, totalMemberCountQuery);

            var spans = searchQuery.Result;

            var totalMemberCount = totalMemberCountQuery.Result.Count;

            return new PageResult<Trace>()
            {
                CurrentPageNumber = traceQuery.CurrentPageNumber,
                PageSize = traceQuery.PageSize,
                TotalMemberCount = (int)totalMemberCount,
                TotalPageCount = (int)Math.Ceiling((double)totalMemberCount / (double)traceQuery.PageSize),
                Data = spans.Documents.GroupBy(x => x.TraceId).Select(x => new Trace { TraceId = x.Key, Spans = x.ToList() })
            };
        }

        public Task<IEnumerable<string>> GetServices()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Span>> GetSpanDependencies(DependencyQuery dependencyQuery)
        {
            throw new System.NotImplementedException();
        }

        private QueryContainer BuildTracesQuery(TraceQuery traceQuery)
        {
            var query = new QueryContainerDescriptor<Span>();
            return query.Bool(b => b.Must(BuildMustQuery(traceQuery)));
        }

        private IEnumerable<Func<QueryContainerDescriptor<Span>, QueryContainer>> BuildMustQuery(TraceQuery traceQuery)
        {
            if (traceQuery.StartTimestamp != null)
            {
                yield return new Func<QueryContainerDescriptor<Span>, QueryContainer>(q => q.DateRange(d => d.Field(x => x.StartTimestamp).GreaterThanOrEquals(traceQuery.StartTimestamp.Value.DateTime)));
            }
            if (traceQuery.FinishTimestamp != null)
            {
                yield return new Func<QueryContainerDescriptor<Span>, QueryContainer>(q => q.DateRange(d => d.Field(x => x.FinishTimestamp).LessThanOrEquals(traceQuery.FinishTimestamp.Value.DateTime)));
            }
        }

        private Indices BuildIndices(DateTimeOffset? startTimestamp, DateTimeOffset? finishTimestamp)
        {
            return null;
        }
    }
}