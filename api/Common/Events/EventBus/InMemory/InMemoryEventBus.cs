
using MediatR;

namespace newFitnet.Common.Events.EventBus.InMemory
{
    internal sealed class InMemoryEventBus(IMediator mediator) : IEventBus
    {
        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IIntegrationEvent
        {
            await mediator.Publish(@event, cancellationToken);
        }
    }
}
