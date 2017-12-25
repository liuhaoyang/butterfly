using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Threading.Tasks;
using AutoMapper;
using Butterfly.Protocol;
using Butterfly.Server.Models;
using Butterfly.Server.ViewModels;
using Butterfly.Storage.Query;

namespace Butterfly.Server.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Span, SpanResponse>().ReverseMap();
            CreateMap<PageResult<Trace>, PageViewModel<TraceViewModel>>()
                .ForMember(destination => destination.PageNumber, option => option.MapFrom(source => source.CurrentPageNumber));
            CreateMap<Trace, TraceViewModel>()
                .ForMember(destination => destination.Duration, option => option.MapFrom(source => GetDuration(source.Spans)))
                .ForMember(destination => destination.StartTimestamp, option => option.MapFrom(source => ToLocalDateTime(source.Spans.Min(x => x.StartTimestamp))))
                .ForMember(destination => destination.FinishTimestamp, option => option.MapFrom(source => ToLocalDateTime(source.Spans.Max(x => x.FinishTimestamp))))
                .ForMember(destination => destination.Services, option => option.Ignore());
            CreateMap<Trace, TraceDetailViewModel>()
//                .ForMember(destination => destination.Spans, option => option.Ignore())
                .ForMember(destination => destination.Duration, option => option.MapFrom(source => GetDuration(source.Spans)))
                .ForMember(destination => destination.StartTimestamp, option => option.MapFrom(source => ToLocalDateTime(source.Spans.Min(x => x.StartTimestamp))))
                .ForMember(destination => destination.FinishTimestamp, option => option.MapFrom(source => ToLocalDateTime(source.Spans.Max(x => x.FinishTimestamp))));
            CreateMap<Span, SpanViewModel>()
//                .ForMember(destination => destination.Childs, option => option.Ignore())
                .ForMember(destination => destination.ServiceName, option => option.MapFrom(span => GetService(span)))
                .ForMember(destination => destination.StartTimestamp, option => option.MapFrom(span => ToLocalDateTime(span.StartTimestamp)))
                .ForMember(destination => destination.FinishTimestamp, option => option.MapFrom(span => ToLocalDateTime(span.FinishTimestamp)));
        }

        private static long GetDuration(IEnumerable<Span> spans)
        {
            var timeSpan = spans.Max(x => x.FinishTimestamp) - spans.Min(x => x.StartTimestamp);
            return timeSpan.GetMicroseconds();
        }

        private static DateTime ToLocalDateTime(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.LocalDateTime;
        }

        private static string GetService(Span span)
        {
            return span.Tags?.FirstOrDefault(x => x.Key == QueryConstants.Service)?.Value;
        }
    }
}