using System;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Flow;

namespace Butterfly.Core
{
//    public class InMemoryHostedConsumer: IHostedConsumer
//    {
//        private readonly IResetEvent _resetEvent;
//        private readonly ISpanConsumer _spanConsumer;
//        private readonly Task _consumerTask;
//
//        public InMemoryHostedConsumer(IResetEvent resetEvent, ISpanConsumer spanConsumer)
//        {
//            _resetEvent = resetEvent ?? throw new ArgumentNullException(nameof(resetEvent));
//            _spanConsumer = spanConsumer ?? throw new ArgumentNullException(nameof(spanConsumer));
//            _consumerTask = null;
//        }
//       
//        public Task Start(CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public Task Stop(CancellationToken cancellationToken)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
}