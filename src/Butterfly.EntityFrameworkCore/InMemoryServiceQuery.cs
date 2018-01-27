using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.DataContract.Tracing;
using Butterfly.Storage;
using Butterfly.Storage.Query;
using Microsoft.EntityFrameworkCore;

namespace Butterfly.EntityFrameworkCore
{
    internal class InMemoryServiceQuery : IServiceQuery
    {
        private readonly InMemoryDbContext _dbContext;
        private readonly IMapper _mapper;

        public InMemoryServiceQuery(InMemoryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<Service>> GetServices(TimeRangeQuery query)
        {
            var services = _dbContext.Tags.AsNoTracking().Where(x => x.Key == QueryConstants.Service).Select(x => x.Value).Distinct().ToList();
            return Task.FromResult(services.Select(x => new Service { Name = x }));
        }
    }
}