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
            var query = _dbContext.Spans.AsNoTracking().Include(x => x.Tags).AsQueryable();
            if (traceQuery.ApplicationName != null)
            {
                var traceIds = query.Where(x => x.Tags.Any(t => t.Key == "application" && t.Value == traceQuery.ApplicationName)).Select(x => x.TraceId).ToList();
                query = query.Where(x => traceIds.Contains(x.TraceId));
            }

            if (traceQuery.StartTimestamp != null)
            {
                query = query.Where(x => x.StartTimestamp <= traceQuery.StartTimestamp);
            }

            if (traceQuery.FinishTimestamp != null)
            {
                query = query.Where(x => x.FinishTimestamp >= traceQuery.FinishTimestamp);
            }

            if (traceQuery.MinDuration != null)
            {
                query = query.Where(x => x.Duration >= traceQuery.MinDuration);
            }

            if (traceQuery.MaxDuration != null)
            {
                query = query.Where(x => x.Duration <= traceQuery.MaxDuration);
            }

            query = query.Skip((traceQuery.CurrentPageNumber - 1) * traceQuery.PageSize).Take(traceQuery.PageSize);

            var totalMemberCount = query.Count();

            return Task.FromResult(new PageResult<Trace>()
            {
                CurrentPageNumber = traceQuery.CurrentPageNumber + 1,
                PageSize = traceQuery.PageSize,
                TotalMemberCount = totalMemberCount,
                TotalPageCount = (int) Math.Ceiling((double) totalMemberCount / (double) traceQuery.PageSize),
                Data = query.ToList().GroupBy(x => x.TraceId).Select(x => new Trace() {TraceId = x.Key, Spans = _mapper.Map<List<Span>>(x.ToList())}).ToList()
            });
        }
    }
}