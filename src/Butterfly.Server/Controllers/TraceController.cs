using System.Threading.Tasks;
using Butterfly.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class TraceController : Controller
    {
        [HttpGet]
        public Task<TraceResponse> Get([FromRoute]string traceId)
        {
            return Task.FromResult(new TraceResponse());
        }
    }
}