using System.Collections.Generic;

namespace Butterfly.APM.OpenTracing
{
    public interface ISpanContextFactory
    {
        ISpanContext Create(SpanContextPackage spanContextPackage);
    }
}