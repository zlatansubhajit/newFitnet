namespace newFitnet.Member
{
    internal static class MembersApiPaths
    {
        private const string MembersRootApi = $"{ApiPaths.Root}/members";

        internal const string Root = MembersRootApi;
        internal const string Delete = $"{MembersRootApi}/{{id}}";
    }
}
