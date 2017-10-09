using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.APM.Core.OpenTracing
{
    public static class SpanBuilderExtensions
    {
        public static ISpanBuilder AsChildOf(this ISpanBuilder spanBuilder, ISpanContext spanContext)
        {
            return spanBuilder.Reference(new SpanReference(SpanReferenceOptions.ChildOf, spanContext));
        }

        public static ISpanBuilder FollowsFrom(this ISpanBuilder spanBuilder, ISpanContext spanContext)
        {
            return spanBuilder.Reference(new SpanReference(SpanReferenceOptions.FollowsFrom, spanContext));
        }
    }
}
