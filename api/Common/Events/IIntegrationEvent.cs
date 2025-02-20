using MediatR;

namespace newFitnet.Common.Events
{
    internal interface IIntegrationEvent : INotification
    {
        Guid Id { get; }
        DateTimeOffset OccuredDateTime { get; }
    }
}
