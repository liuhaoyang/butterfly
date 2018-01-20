using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Butterfly.Common;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage.Query;
using Nest;
using ISpanQuery = Butterfly.Storage.ISpanQuery;

namespace Butterfly.Elasticsearch
{
    public class ElasticsearchSpanQuery : ISpanQuery
    {
        private readonly ElasticClient _elasticClient;
        private readonly IIndexManager _indexManager;

        public ElasticsearchSpanQuery(IElasticClientFactory elasticClientFactory, IIndexManager indexManager)
        {
            _indexManager = indexManager;
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
            var index = Indices.Index(_indexManager.CreateIndex(null));

            var query = BuildTracesQuery(traceQuery);

            var spans = await _elasticClient.SearchAsync<Span>(search => search.Index(index).Query(query).Size(10).Sort(sort => sort.Descending(x => x.StartTimestamp)));

            var traces = spans.Documents.GroupBy(x => x.TraceId);

            var totalMemberCount = traces.Count();
            
            //var ss 

            return new PageResult<Trace>()
            {
                CurrentPageNumber = traceQuery.CurrentPageNumber,
                PageSize = traceQuery.PageSize,
                TotalMemberCount = totalMemberCount,
                TotalPageCount = (int) Math.Ceiling((double) totalMemberCount / (double) traceQuery.PageSize),
                Data = traces.Skip((traceQuery.CurrentPageNumber - 1) * traceQuery.PageSize).Take(traceQuery.PageSize).Select(x => new Trace {TraceId = x.Key, Spans = x.ToList()})
            };
        }

        public async Task<IEnumerable<string>> GetServices()
        {
            var index = Indices.Index(_indexManager.CreateIndex(null));
            var spans = await _elasticClient.SearchAsync<Span>(s => s.Index(index).Source(x => x
                .Includes(i => i.Fields("tags.key", "tags.value"))).Query(q => q.Nested(n => n.Path(x => x.Tags).Query(q1 => q1.Term(new Field("tags.key"), QueryConstants.Service)))));
            return spans.Documents.Select(ServiceUtils.GetService).Distinct().ToArray();
        }

        public Task<IEnumerable<Span>> GetSpanDependencies(DependencyQuery dependencyQuery)
        {
            throw new System.NotImplementedException();
        }

        private Func<QueryContainerDescriptor<Span>, QueryContainer> BuildTracesQuery(TraceQuery traceQuery)
        {
            return query => query.Bool(b => b.Must(BuildMustQuery(traceQuery)));
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
                yield return q => q.Nested(n => n.Path(x => x.Tags).Query(q1 => q1.Bool(b => b.Must(f => f.Term(new Field("tags.key"), queryTag.Key?.ToLower()), f => f.Term(new Field("tags.value"), queryTag.Value?.ToLower())))));
            }
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