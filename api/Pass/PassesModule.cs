using newFitnet.Pass.Data.Database;

namespace newFitnet.Pass
{
    internal static class PassesModule
    {
        internal static IServiceCollection AddPasses(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            return services;
        }

        internal static IApplicationBuilder UsePasses(this IApplicationBuilder app)
        {
            app.UseDatabase();
            return app;
        }
    }
}
