namespace Butterfly.APM.OpenTracing
{
    public interface ISampler
    {
        bool ShouldSample();
    }
}