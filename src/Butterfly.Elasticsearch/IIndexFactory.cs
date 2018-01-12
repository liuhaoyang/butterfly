using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Elasticsearch
{
    public interface IIndexFactory
    {
        string CreateTracingIndex();
    }
}
