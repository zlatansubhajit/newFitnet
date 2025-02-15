namespace newFitnet.Member
{
    using Data.Database;
    internal static class MembersModule
    {
        internal static IServiceCollection AddMembers(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDatabase(configuration);
            return services;
        }


        internal static IApplicationBuilder UseMembers(this IApplicationBuilder app) 
        {
            app.UseDatabase();
            return app;
        }
    }
}
