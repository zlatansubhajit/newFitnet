using MediatR;

namespace newFitnet.Common.Events
{
    internal interface IIntegrationEventHandler<in TEvent>: INotificationHandler<TEvent> where TEvent: IIntegrationEvent;
}
