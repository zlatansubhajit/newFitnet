using Microsoft.EntityFrameworkCore;

namespace newFitnet.Pass.Data.Database
{
    internal sealed class PassPersistence(DbContextOptions<PassPersistence> options) : DbContext(options)
    {
        private const string Schema = "Pass";


        public DbSet<Pass> Passes => Set<Pass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PassEntityConfiguration());

        }
    }
}
