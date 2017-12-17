using System;
using System.Threading;
using System.Threading.Tasks;
using Butterfly.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Butterfly.Flow.InMemory
{
    public class InMemoryFlowService : IFlowService
    {
        private const int DEFAUKT_CONSUMER = 1;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISpanConsumer _spanConsumer;
        private readonly Task[] _consumerTasks;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly InMemoryFlowOptions _inMemoryFlowOptions;

        public InMemoryFlowService(IServiceProvider serviceProvider, ISpanConsumer spanConsumer, IOptions<InMemoryFlowOptions> options)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _spanConsumer = spanConsumer ?? throw new ArgumentNullException(nameof(spanConsumer));
            _inMemoryFlowOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _consumerTasks = new Task[_inMemoryFlowOptions.MaxConsumer == 0 ? DEFAUKT_CONSUMER : _inMemoryFlowOptions.MaxConsumer];
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            for (var i = 0; i < _consumerTasks.Length; i++)
            {
                _consumerTasks[i] = Task.Factory.StartNew(async () => await ConsumerAction(),
                    _cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }

            return TaskUtils.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            return TaskUtils.CompletedTask;
        }

        private async Task ConsumerAction()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var callBack = scope.ServiceProvider.GetRequiredService<ISpanConsumerCallback>();
                await _spanConsumer.AcceptAsync(callBack, _cancellationTokenSource.Token);
            }
        }
    }
}