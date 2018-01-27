using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.Streaming;
using Butterfly.DataContract.Tracing;
using Butterfly.Server.ViewModels;
using Butterfly.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class SpanController : Controller
    {
        private readonly ISpanProducer _spanProducer;
        private readonly ISpanQuery _spanQuery;
        private readonly IMapper _mapper;

        public SpanController(ISpanProducer spanProducer, ISpanQuery spanQuery, IMapper mapper)
        {
            _spanProducer = spanProducer;
            _spanQuery = spanQuery;
            _mapper = mapper;
        }

        [HttpGet("{spanId}")]
        public async Task<SpanDetailViewModel> Get([FromRoute] string spanId)
        {
            var span = _mapper.Map<SpanDetailViewModel>(await _spanQuery.GetSpan(spanId));
            span.Logs = span.Logs.OrderBy(x => x.Timestamp).ToList();
            return span;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Span[] spans)
        {
            if (spans != null)
            {
                _spanProducer.PostAsync(spans, CancellationToken.None);
            }
            return StatusCode(StatusCodes.Status202Accepted);
        }
    }
}