using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.Protocol;
using Butterfly.Storage;
using Butterfly.Storage.Query;
using Microsoft.EntityFrameworkCore;

namespace Butterfly.EntityFrameworkCore
{
    internal class InMemorySpanQuery : ISpanQuery
    {
        private readonly InMemoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public IQueryable<SpanModel> _spanQuery
        {
            get { return _dbContext.Spans.AsNoTracking().Include(x => x.Baggages).Include(x => x.Tags).Include(x => x.References).Include(x => x.Logs); }
        }

        public InMemorySpanQuery(InMemoryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<Span>> GetSpans()
        {
            return Task.FromResult(_mapper.Map<IEnumerable<Span>>(_spanQuery.ToList()));
        }

        public Task<IEnumerable<Span>> GetTrace(string traceId)
        {
            return Task.FromResult(_mapper.Map<IEnumerable<Span>>(_spanQuery.Where(x => x.TraceId == traceId).ToList()));
        }

        public Task<PageResult<Trace>> GetTraces(TraceQuery traceQuery)
        {
            var query = _dbContext.Spans.AsNoTracking().Include(x => x.Tags).OrderByDescending(x => x.StartTimestamp).AsQueryable();
            if (traceQuery.ServiceName != null)
            {
                var traceIds = query.Where(x => x.Tags.Any(t => t.Key == QueryConstants.Service && t.Value == traceQuery.ServiceName))
                    .Select(x => x.TraceId).Distinct().ToList();
                query = query.Where(x => traceIds.Contains(x.TraceId));
            }

            if (traceQuery.StartTimestamp != null)
            {
                query = query.Where(x => x.StartTimestamp >= traceQuery.StartTimestamp);
            }

            if (traceQuery.FinishTimestamp != null)
            {
                query = query.Where(x => x.FinishTimestamp <= traceQuery.FinishTimestamp);
            }

            var queryGroup = query.ToList().GroupBy(x => x.TraceId);

            if (traceQuery.MinDuration != null)
            {
                queryGroup = queryGroup.Where(x => x.Sum(s => s.Duration) >= traceQuery.MinDuration);
            }

            if (traceQuery.MaxDuration != null)
            {
                queryGroup = queryGroup.Where(x => x.Sum(s => s.Duration) <= traceQuery.MaxDuration);
            }

            var totalMemberCount = queryGroup.Count();

            queryGroup = queryGroup.Skip((traceQuery.CurrentPageNumber - 1) * traceQuery.PageSize).Take(traceQuery.PageSize);

            return Task.FromResult(new PageResult<Trace>()
            {
                CurrentPageNumber = traceQuery.CurrentPageNumber,
                PageSize = traceQuery.PageSize,
                TotalMemberCount = totalMemberCount,
                TotalPageCount = (int) Math.Ceiling((double) totalMemberCount / (double) traceQuery.PageSize),
                Data = queryGroup.ToList().Select(x => new Trace() {TraceId = x.Key, Spans = _mapper.Map<List<Span>>(x.ToList())}).ToList()
            });
        }

        public Task<IEnumerable<string>> GetServices()
        {
            var services = _dbContext.Tags.AsNoTracking().Where(x => x.Key == QueryConstants.Service).Select(x => x.Value).Distinct().ToList();
            return Task.FromResult((IEnumerable<string>) services);
        }
    }
}