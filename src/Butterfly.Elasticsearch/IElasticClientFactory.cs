using Nest;

namespace Butterfly.Elasticsearch
{
    public interface IElasticClientFactory
    {
        ElasticClient Create();
    }
}