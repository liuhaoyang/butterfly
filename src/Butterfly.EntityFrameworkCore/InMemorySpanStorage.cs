using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.Storage;
using Butterfly.Protocol;

namespace Butterfly.EntityFrameworkCore
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class InMemorySpanStorage : ISpanStorage
    {
        private readonly InMemoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public InMemorySpanStorage(InMemoryDbContext dbContext, IMapper mapper)
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
    }
}