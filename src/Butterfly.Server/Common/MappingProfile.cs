using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(destination => destination.Duration, option => option.MapFrom(source => source.Spans.Sum(x => x.Duration)))
                .ForMember(destination => destination.StartTimestamp, option => option.MapFrom(source => source.Spans.Min(x => x.StartTimestamp).LocalDateTime))
                .ForMember(destination => destination.FinishTimestamp, option => option.MapFrom(source => source.Spans.Max(x => x.FinishTimestamp).LocalDateTime))
                .ForMember(destination => destination.Services, option => option.MapFrom(source => GetTraceApplicationsFromTrace(source)));
        }

        private static List<TraceService> GetTraceApplicationsFromTrace(Trace trace)
        {
            var traceApplications = new List<TraceService>();
            foreach (var span in trace.Spans)
            {
                var applicationTag = span.Tags.FirstOrDefault(x => x.Key == QueryConstants.Service);
                traceApplications.Add(new TraceService(applicationTag?.Value));
            }

            return traceApplications;
        }
    }
}