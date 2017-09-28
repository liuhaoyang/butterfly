namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanContext
    {
        ReadOnlyBaggage GetBaggage();
    }
}