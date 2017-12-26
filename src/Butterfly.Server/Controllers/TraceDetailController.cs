using System.Threading.Tasks;
using AutoMapper;
using Butterfly.Server.ViewModels;
using Butterfly.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]/{traceId}")]
    public class TraceDetailController : Controller
    {
        private readonly ISpanQuery _spanQuery;
        private readonly IMapper _mapper;

        public TraceDetailController(ISpanQuery spanQuery, IMapper mapper)
        {
            _spanQuery = spanQuery;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<TraceDetailViewModel> Get([FromRoute] string traceId)
        {
            var trace = await _spanQuery.GetTrace(traceId);
            var result = _mapper.Map<TraceDetailViewModel>(trace);

            foreach (var span in result.Spans)
            {
                var offsetTimespan = span.StartTimestamp - result.StartTimestamp;
                span.Offset = offsetTimespan.GetMicroseconds();
            }

            return result;
        }
    }
}