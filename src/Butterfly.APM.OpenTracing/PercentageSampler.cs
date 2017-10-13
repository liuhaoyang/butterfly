using System;

namespace Butterfly.APM.OpenTracing
{
    public class PercentageSampler : ISampler
    {
        private readonly Random _random = new Random();
        private readonly int _samplingRate;

        public PercentageSampler(int samplingRate)
        {
            _samplingRate = samplingRate;
        }

        public bool ShouldSample()
        {
            if (_samplingRate >= 100)
            {
                return true;
            }
            var random = _random.Next(1, 100);
            return random < _samplingRate;
        }
    }
}