using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SocialNetWork.DAL.Models.Users;

namespace SocialNetWork.DAL.Configurations
{
    public class MessageConfuiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
