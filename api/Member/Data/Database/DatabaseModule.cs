using Microsoft.EntityFrameworkCore;

namespace newFitnet.Member.Data.Database
{
    internal static class DatabaseModule
    {
        internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionstring = configuration.GetConnectionString("Members");
            services.AddDbContext<MembersPersistence>(options => options.UseNpgsql(connectionstring));
            return services;
        }


        internal static IApplicationBuilder UseDatabase(this IApplicationBuilder app) 
        {
            app.UseAutomaticMigrations();
            return app;
        }
    }
}
