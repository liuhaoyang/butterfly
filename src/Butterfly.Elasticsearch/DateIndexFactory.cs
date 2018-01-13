using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Elasticsearch
{
    internal class DateIndexFactory : IIndexFactory
    {
        private const string TracingIndexSuffix = "tracing";

        public string CreateTracingIndex()
        {
            return $"{DateTimeOffset.UtcNow:yyyyMMdd}-{TracingIndexSuffix}";
        }
    }
}