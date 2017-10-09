using System.Collections.Generic;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanContextFactory
    {
        ISpanContext Create(SpanContextPackage spanContextPackage);
    }
}