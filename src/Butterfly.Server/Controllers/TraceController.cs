using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.Server.Models;
using Butterfly.Server.ViewModels;
using Butterfly.Storage;
using Butterfly.Storage.Query;
using Microsoft.AspNetCore.Mvc;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class TraceController : Controller
    {
        private readonly ISpanQuery _spanQuery;
        private readonly IMapper _mapper;

        public TraceController(ISpanQuery spanQuery, IMapper mapper)
        {
            _spanQuery = spanQuery;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<PageViewModel<TraceViewModel>> Get(
            [FromQuery] string service,
            [FromQuery] DateTime? startTimestamp, [FromQuery] DateTime? finishTimestamp,
            [FromQuery] int? minDuration, [FromQuery] int? maxDuration,
            [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var query = new TraceQuery
            {
                ServiceName = service,
                StartTimestamp = startTimestamp?.ToUniversalTime(),
                FinishTimestamp = finishTimestamp?.ToUniversalTime(),
                MinDuration = minDuration,
                MaxDuration = maxDuration,
                CurrentPageNumber = pageNumber.GetValueOrDefault(1),
                PageSize = pageSize.GetValueOrDefault(10)
            };
            var data = await _spanQuery.GetTraces(query);
            return _mapper.Map<PageViewModel<TraceViewModel>>(data);
        }
    }
}