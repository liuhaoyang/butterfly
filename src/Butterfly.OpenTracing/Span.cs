using System;
using System.Diagnostics;
using System.Threading;

namespace Butterfly.OpenTracing
{
    internal class Span : ISpan
    {
        private readonly ISpanRecorder _spanChannel;
        private long _startTimestamp;
        private long _finishTimestamp;
        private int _state;

        public long StartTimestamp => _startTimestamp;

        public long FinishTimestamp => _finishTimestamp;

        public long Duration
        {
            get
            {
                if (_state != 1)
                {
                    var timestamp = Stopwatch.GetTimestamp();
                    return timestamp - _startTimestamp;
                }
                return _finishTimestamp - _startTimestamp;
            }
        }

        public ISpanContext SpanContext { get; }

        public Baggage Baggage { get; }

        public TagCollection Tags { get; }

        public string OperationName { get; }

        public Span(string operationName, ISpanContext spanContext, ISpanRecorder spanChannel)
        {
            SpanContext = spanContext ?? throw new ArgumentNullException(nameof(spanContext));
            Baggage = spanContext.Baggage;
            Tags = new TagCollection();
            OperationName = operationName;

            _spanChannel = spanChannel ?? throw new ArgumentNullException(nameof(spanChannel));
            _state = 0;
            _startTimestamp = Stopwatch.GetTimestamp();
        }

        public void Dispose()
        {
            Finish();
        }

        public void Finish()
        {
            if (Interlocked.CompareExchange(ref _state, 1, 0) != 1)
            {
                _finishTimestamp = Stopwatch.GetTimestamp();
                _spanChannel.RecordAsync(this);
            }
        }
    }
}