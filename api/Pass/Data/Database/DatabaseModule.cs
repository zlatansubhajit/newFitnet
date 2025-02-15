using Microsoft.EntityFrameworkCore;

namespace newFitnet.Pass.Data.Database
{
    internal static class DatabaseModule
    {
        internal static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("Passes");
            services.AddDbContext<PassPersistence>(options => options.UseNpgsql(connectionstring));
            return services;
        }

        internal static IApplicationBuilder UseDatabase(this IApplicationBuilder app)
        {
            app.UseAutomaticMigrations();
            return app;
        }
    }
}
