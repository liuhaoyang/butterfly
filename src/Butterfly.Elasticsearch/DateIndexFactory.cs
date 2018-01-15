using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Elasticsearch
{
    internal class DateIndexFactory : IIndexFactory
    {
        private const string IndexSuffix = "butterfly";

        public string CreateIndex()
        {
            return $"{IndexSuffix}-{DateTimeOffset.UtcNow:yyyyMMdd}";
        }
    }
}