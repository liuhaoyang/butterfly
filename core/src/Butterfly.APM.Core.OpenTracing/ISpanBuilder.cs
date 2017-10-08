namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanBuilder
    {
        ISpanBuilder Reference(SpanReferenceOptions reference, ISpanContext spanContext);

        string OperationName { get; }
    }
}