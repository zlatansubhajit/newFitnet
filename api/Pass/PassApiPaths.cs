namespace newFitnet.Pass
{
    internal static class PassApiPaths
    {
        private const string PassesRootApi = $"{ApiPaths.Root}/passes";
        internal const string Root = PassesRootApi;
        internal const string Specific = $"{PassesRootApi}/{{id}}";
    }
}
