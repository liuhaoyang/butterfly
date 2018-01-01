using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Butterfly.DataContract.Tracing;
using Butterfly.Server.ViewModels;
using Butterfly.Storage;
using Butterfly.Storage.Query;
using Microsoft.AspNetCore.Mvc;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class DependencyController : Controller
    {
        private const string unknownService = "unknown";
        private readonly ISpanQuery _spanQuery;

        public DependencyController(ISpanQuery spanQuery)
        {
            _spanQuery = spanQuery;
        }

        [HttpGet]
        public async Task<DependencyViewModel> Get([FromQuery] DateTime? startTimestamp, [FromQuery] DateTime? finishTimestamp)
        {
            var spans = await _spanQuery.GetSpanDependencies(
                new DependencyQuery {StartTimestamp = startTimestamp?.ToUniversalTime(), FinishTimestamp = finishTimestamp?.ToUniversalTime()});

            var dependency = new DependencyViewModel();

            dependency.Nodes = GetNodes(spans).ToList();

            var dependencies = spans.Where(x => x.References.Any(r => r.Reference == "ChildOf")).GroupBy(x =>
            {
                var @ref = x.References.First();
                var parent = spans.FirstOrDefault(s => s.SpanId == @ref.ParentId);
                return new {source = GetService(parent), target = GetService(x)};
            });

            foreach (var item in dependencies)
            {
                if (item.Key.source == item.Key.target)
                {
                    continue;
                }

                dependency.Edges.Add(new EdgeViewModel
                {
                    Source = item.Key.source,
                    Target = item.Key.target,
                    Value = item.Count()
                });
            }

            return dependency;
        }

        private string GetService(Span span)
        {
            return span?.Tags?.FirstOrDefault(x => x.Key == QueryConstants.Service)?.Value ?? unknownService;
        }

        private IEnumerable<NodeViewModel> GetNodes(IEnumerable<Span> spans)
        {
            foreach (var service in spans.GroupBy(GetService))
            {
                yield return new NodeViewModel {Name = service.Key};
            }
        }
    }
}