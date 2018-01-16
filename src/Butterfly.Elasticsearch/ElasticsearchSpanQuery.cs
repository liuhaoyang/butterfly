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

            var searchQuery = _elasticClient.SearchAsync<Span>(search => search.Index(index).Query(query).From((traceQuery.CurrentPageNumber - 1) * traceQuery.PageSize).Size(traceQuery.PageSize).Sort(sort => sort.Descending(x => x.StartTimestamp)));

            var totalMemberCountQuery = _elasticClient.CountAsync<Span>(c => c.Index(index).Query(query));

            await Task.WhenAll(searchQuery, totalMemberCountQuery);

            var spans = searchQuery.Result;

            var totalMemberCount = totalMemberCountQuery.Result.Count;

            return new PageResult<Trace>()
            {
                CurrentPageNumber = traceQuery.CurrentPageNumber,
                PageSize = traceQuery.PageSize,
                TotalMemberCount = (int) totalMemberCount,
                TotalPageCount = (int) Math.Ceiling((double) totalMemberCount / (double) traceQuery.PageSize),
                Data = spans.Documents.GroupBy(x => x.TraceId).Select(x => new Trace {TraceId = x.Key, Spans = x.ToList()})
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

        private Func<QueryContainerDescriptor<Span>, QueryContainer> BuildTracesQuery(TraceQuery traceQuery)
        {
            return query => query.Bool(b => b.Filter(BuildMustQuery(traceQuery)));
        }

        private IEnumerable<Func<QueryContainerDescriptor<Span>, QueryContainer>> BuildMustQuery(TraceQuery traceQuery)
        {
            if (traceQuery.StartTimestamp != null)
            {
                yield return q => q.DateRange(d => d.Field(x => x.StartTimestamp).GreaterThanOrEquals(traceQuery.StartTimestamp.Value.DateTime));
            }

            if (traceQuery.FinishTimestamp != null)
            {
                yield return q => q.DateRange(d => d.Field(x => x.FinishTimestamp).LessThanOrEquals(traceQuery.FinishTimestamp.Value.DateTime));
            }

            foreach (var queryTag in BuildQueryTags(traceQuery))
            {
                yield return q => q.Bool(b => b.Filter(f => f.Term(new Field("tags.key"), queryTag.Key), f => f.Term(new Field("tags.value"), queryTag.Value)));
            }
        }

        private Indices BuildIndices(DateTimeOffset? startTimestamp, DateTimeOffset? finishTimestamp)
        {
            return Indices.Index(_indexFactory.CreateIndex());
        }

        private IEnumerable<Tag> BuildQueryTags(TraceQuery traceQuery)
        {
            if (!string.IsNullOrEmpty(traceQuery.ServiceName))
            {
                yield return new Tag {Key = QueryConstants.Service, Value = traceQuery.ServiceName};
            }

            if (!string.IsNullOrEmpty(traceQuery.Tags))
            {
                var tags = traceQuery.Tags.Split('|');
                foreach (var tag in tags)
                {
                    var pair = tag.Split('=');
                    if (pair.Length == 2)
                    {
                        yield return new Tag {Key = pair[0], Value = pair[1]};
                    }
                }
            }
        }
    }
}