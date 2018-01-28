using Microsoft.Extensions.Options;

namespace Butterfly.Elasticsearch
{
    public class ElasticsearchOptions: IOptions<ElasticsearchOptions>
    {
        public ElasticsearchOptions Value { get; }
        
        public string ElasticsearchHosts{ get; set; }
    }
}