using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.Server.ViewModels;
using Butterfly.Storage;
using Butterfly.Storage.Query;
using Microsoft.AspNetCore.Mvc;
using  Butterfly.Protocol;
using  System.Linq;

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
            var pageViewModel = _mapper.Map<PageViewModel<TraceViewModel>>(data);

            foreach (var traceViewModel in pageViewModel.Data)
            {
                var trace = data.Data.FirstOrDefault(x => x.TraceId == traceViewModel.TraceId);
                traceViewModel.Services = GetTraceServices(trace);
            }

            return pageViewModel;
        }

        private List<TraceService> GetTraceServices(Trace trace)
        {
            var traceApplications = new List<TraceService>();
            foreach (var span in trace.Spans)
            {
                var applicationTag = span.Tags.FirstOrDefault(x => x.Key == QueryConstants.Service);
                traceApplications.Add(new TraceService(applicationTag?.Value));
            }

            return traceApplications;
        }
    }
}