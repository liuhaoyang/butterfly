namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISampler
    {
        bool ShouldSample();
    }
}