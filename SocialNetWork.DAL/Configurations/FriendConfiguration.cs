using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SocialNetWork.DAL.Models.Users;

namespace SocialNetWork.DAL.Configurations
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {

        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("UserFriends").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();    
        }
    }
}
