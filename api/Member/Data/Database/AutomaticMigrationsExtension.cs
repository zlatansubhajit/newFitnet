using Microsoft.EntityFrameworkCore;

namespace newFitnet.Member.Data.Database
{
    internal static class AutomaticMigrationsExtension
    {

        internal static IApplicationBuilder UseAutomaticMigrations(this IApplicationBuilder app) 
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MembersPersistence>();
            context.Database.Migrate();
            return app;
        }
    }
}
