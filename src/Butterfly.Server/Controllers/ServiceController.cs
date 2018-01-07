using System.Collections.Generic;
using System.Threading.Tasks;
using Butterfly.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        // GET
        [HttpGet]
        public Task<IEnumerable<string>> Get([FromServices]ISpanQuery spanQuery)
        {
            return spanQuery.GetServices();
        }
    }
}