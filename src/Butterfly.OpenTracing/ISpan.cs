using System;

namespace Butterfly.OpenTracing
{
    public interface ISpan : IDisposable
    {
        DateTime StartTimestamp { get; }

        DateTime FinishTimestamp { get; }

        string OperationName { get; }

        ISpanContext SpanContext { get; }

        TagCollection Tags { get; }

        void Finish();
    }
}