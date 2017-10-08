using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.APM.Core.OpenTracing
{
    public static class SpanBuilderExtensions
    {
        public static ISpanBuilder AsChildOf(this ISpanBuilder spanBuilder, ISpanContext spanContext)
        {
            if (spanBuilder == null)
            {
                throw new ArgumentNullException(nameof(spanBuilder));
            }
            return spanBuilder.Reference(SpanReferenceOptions.ChildOf, spanContext);
        }

        public static ISpanBuilder FollowsFrom(this ISpanBuilder spanBuilder, ISpanContext spanContext)
        {
            if (spanBuilder == null)
            {
                throw new ArgumentNullException(nameof(spanBuilder));
            }
            return spanBuilder.Reference(SpanReferenceOptions.FollowsFrom, spanContext);
        }
    }
}
