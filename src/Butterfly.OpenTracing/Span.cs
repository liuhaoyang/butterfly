using System;
using System.Threading;

namespace Butterfly.OpenTracing
{
    internal class Span : ISpan
    {
        private readonly ISpanRecorder _spanChannel;
        private DateTime _finishTimestamp;
        private int _state;

        public DateTime StartTimestamp { get; }

        public DateTime FinishTimestamp => _finishTimestamp;

        public ISpanContext SpanContext { get; }

        public TagCollection Tags { get; }
        
        public LogCollection Logs { get; }

        public string OperationName { get; }

        public Span(string operationName, ISpanContext spanContext, ISpanRecorder spanChannel)
        {
            _state = 0;
            _spanChannel = spanChannel ?? throw new ArgumentNullException(nameof(spanChannel));
            SpanContext = spanContext ?? throw new ArgumentNullException(nameof(spanContext));
            Tags = new TagCollection();
            Logs = new LogCollection();
            OperationName = operationName;
            StartTimestamp = DateTime.UtcNow;
        }

        public void Dispose()
        {
            Finish();
        }

        public void Finish()
        {
            if (Interlocked.CompareExchange(ref _state, 1, 0) != 1)
            {
                _finishTimestamp = DateTime.UtcNow;
                _spanChannel.Record(this);
            }
        }
    }
}