using Microsoft.EntityFrameworkCore;

namespace newFitnet.Member.Data.Database
{
    internal sealed class MembersPersistence(DbContextOptions<MembersPersistence> options): DbContext(options)
    {
        private const string Schema = "Members";
        public DbSet<Member> Members => Set<Member>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfiguration(new MemberEntityConfiguration());
        }
    }
}
