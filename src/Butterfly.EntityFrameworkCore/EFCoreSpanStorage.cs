using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers.Internal;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.Storage;
using Butterfly.Protocol;
using Microsoft.EntityFrameworkCore;

namespace Butterfly.EntityFrameworkCore
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EFCoreSpanStorage : ISpanStorage
    {
        private readonly ButterflyDbContext _dbContext;
        private readonly IMapper _mapper;

        public EFCoreSpanStorage(ButterflyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task StoreAsync(IEnumerable<Span> spans, CancellationToken cancellationToken)
        {
            if (spans == null)
            {
                throw new ArgumentNullException(nameof(spans));
            }

            var spanModels = _mapper.Map<IEnumerable<Span>, IEnumerable<SpanModel>>(spans);
            foreach (var spanModel in spanModels)
            {
                if (spanModel.Logs != null)
                {
                    foreach (var log in spanModel.Logs)
                    {
                        var logId = Guid.NewGuid().ToString();
                        log.LogId = logId;
                        log.Fields?.ForEach(field => field.LogId = logId);
                    }
                }

                _dbContext.Spans.Add(spanModel);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public Task<IEnumerable<Span>> GetAll()
        {
            var models = _dbContext.Spans.Include(x => x.Baggages).Include(x => x.Tags).Include(x => x.References).Include(x => x.Logs).ToList();
            return Task.FromResult(_mapper.Map<IEnumerable<SpanModel>, IEnumerable<Span>>(models));
        }
    }
}