using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Flow;
using Butterfly.Protocol;
using Butterfly.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class SpanController : Controller
    {
        private readonly ISpanPublisher _spanPublisher;
        private readonly ISpanStorage _spanStorage;

        public SpanController(ISpanPublisher spanPublisher, ISpanStorage spanStorage)
        {
            _spanPublisher = spanPublisher;
            _spanStorage = spanStorage;
        }

        [HttpGet]
        public Task<IEnumerable<Span>> Get()
        {
            return _spanStorage.GetAll();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Span[] spans)
        {
            _spanPublisher.PostAsync(spans, CancellationToken.None);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}