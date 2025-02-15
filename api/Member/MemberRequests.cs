namespace newFitnet.Member
{
    public sealed record CreateOrUpdateMemberRequest(string name, string phone,string? email, string? address);

}
