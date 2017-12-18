using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.Protocol;
using Butterfly.Storage;
using Microsoft.EntityFrameworkCore;

namespace Butterfly.EntityFrameworkCore
{
    internal class InMemorySpanQuery : ISpanQuery
    {
        private readonly InMemoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public IQueryable<SpanModel> _spanQuery
        {
            get
            {
                return _dbContext.Spans.Include(x => x.Baggages).Include(x => x.Tags).Include(x => x.References).Include(x => x.Logs);
            }
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
    }
}