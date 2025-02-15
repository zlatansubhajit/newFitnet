using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace newFitnet.Pass.Data.Database
{
    internal sealed class PassEntityConfiguration : IEntityTypeConfiguration<Pass>
    {
        public void Configure(EntityTypeBuilder<Pass> builder)
        {
            builder.HasKey(pass => pass.Id);
            builder.Property(pass => pass.MemberId).IsRequired();
            builder.Property(pass => pass.Start).IsRequired();
            builder.Property(pass => pass.End).IsRequired();
            builder.Property(pass => pass.Location).IsRequired();
            builder.Property(pass => pass.Type).HasConversion<string>().IsRequired();
        }
    }
}
