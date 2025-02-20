using newFitnet.Common.Events;
using newFitnet.Member.Data;

namespace newFitnet.Member.Events
{
    internal record NewMemberEvent(
        Guid MemberId,
        string Name,
        string Phone,
        string? Email,
        string? Address,
        DateTimeOffset OccuredAt) : IIntegrationEvent
    {
        public Guid Id => MemberId;

        public DateTimeOffset OccuredDateTime => OccuredAt;

        internal static NewMemberEvent Create(
            Guid memberId,
            string name,
            string phone,
            string? email,
            string? address,
            DateTimeOffset occuredAt) =>
            new(memberId, name, phone, email, address, occuredAt);
    }
}
