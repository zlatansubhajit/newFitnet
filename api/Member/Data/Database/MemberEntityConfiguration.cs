using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace newFitnet.Member.Data.Database
{
    internal sealed class MemberEntityConfiguration : IEntityTypeConfiguration<Member>
    {

        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(member => member.Id);
            builder.Property(member => member.Name).IsRequired();
            builder.Property(member => member.Phone).IsRequired();
        }
    }
}
