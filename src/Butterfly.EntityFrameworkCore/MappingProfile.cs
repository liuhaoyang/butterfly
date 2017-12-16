using AutoMapper;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.Protocol;

namespace Butterfly.EntityFrameworkCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Baggage, BaggageModel>();
            CreateMap<LogField, LogFieldModel>();
            CreateMap<Log, LogModel>();
            CreateMap<Span, SpanModel>();
            CreateMap<SpanReference, SpanReferenceModel>();
            CreateMap<Tag, TagModel>();
        }
    }
}