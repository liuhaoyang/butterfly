using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Elasticsearch
{
    internal class DateIndexFactory : IIndexFactory
    {
        private const string TracingIndexSuffix= "Tracing";

        public string CreateTracingIndex()
        {
            return $"{DateTime.Now.ToString("yyyyMMdd")}_{TracingIndexSuffix}";
        }
    }
}
