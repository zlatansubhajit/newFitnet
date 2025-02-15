using newFitnet.Pass.Data;

namespace newFitnet.Pass
{
    public sealed record RegisterPassRequest(Guid MemberId, DateTimeOffset Start
        , DateTimeOffset End, string Location,
        PassType Type);
}
