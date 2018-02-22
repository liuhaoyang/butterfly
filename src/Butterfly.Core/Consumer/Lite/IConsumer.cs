using System.Threading.Tasks;

namespace Butterfly.Consumer.Lite
{
    public interface IConsumer
    {
        Task Executing();

        Task Complete();
    }
}
