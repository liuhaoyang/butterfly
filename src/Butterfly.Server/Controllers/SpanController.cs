using System;
using System.Collections.Generic;
using AutoMapper;
using Butterfly.EntityFrameworkCore;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.Protocol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using  System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Butterfly.Server.Controllers
{
    [Route("api/[controller]")]
    public class SpanController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ButterflyDbContext _context;

        public SpanController(IMapper mapper, ButterflyDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<SpanModel> Get()
        {
            var span = new SpanModel()
            {
                SpanId = Guid.NewGuid().ToString()
            };
            span.StartTimestamp = DateTimeOffset.UtcNow;
            span.FinishTimestamp = DateTimeOffset.UtcNow;

            span.Tags = new List<TagModel>();

            span.Tags.Add(new TagModel()
            {
                SpanId = span.SpanId,
                Key = "test",
                Value = "testV"
            });
            
            span.Tags.Add(new TagModel()
            {
                SpanId = span.SpanId,
                Key = "aaa",
                Value = "bbb"
            });

            _context.Spans.Add(span);

            _context.SaveChanges();

            var spans = _context.Spans.Include(x=> x.Tags).ToList();
            return spans;
        }

        [HttpPost]
        public IActionResult Post(IEnumerable<Span> spans)
        {
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}