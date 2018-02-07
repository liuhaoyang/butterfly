using System.Threading;
using Butterfly.Consumer;
using Butterfly.DataContract.Tracing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Butterfly.HttpCollector.Controllers
{
    [EnableHttpCollector]
    [Route("api/Span")]
    public class SpanController : Controller
    {
        private readonly ISpanProducer _spanProducer;

        public SpanController(ISpanProducer spanProducer)
        {
            _spanProducer = spanProducer;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Span[] spans)
        {
            if (spans != null)
            {
                _spanProducer.PostAsync(spans, CancellationToken.None);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}