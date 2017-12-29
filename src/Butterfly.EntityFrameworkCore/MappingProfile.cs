using AutoMapper;
using Butterfly.EntityFrameworkCore.Models;
using Butterfly.DataContract.Tracing;

namespace Butterfly.EntityFrameworkCore
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Baggage, BaggageModel>().ReverseMap();
            CreateMap<LogField, LogFieldModel>().ReverseMap();
            CreateMap<Log, LogModel>().ReverseMap();
            CreateMap<Span, SpanModel>().ReverseMap();
            CreateMap<SpanReference, SpanReferenceModel>().ReverseMap();
            CreateMap<Tag, TagModel>().ReverseMap();
        }
    }
}