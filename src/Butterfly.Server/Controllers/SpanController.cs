using Microsoft.AspNetCore.Mvc;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class SpanController : Controller
    {
        public string Get()
        {
            return "span";
        }
    }
}