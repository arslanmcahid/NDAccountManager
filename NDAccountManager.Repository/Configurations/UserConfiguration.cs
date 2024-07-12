using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDAccountManager.Core.Models;

namespace NDAccountManager.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);  
            builder.Property(x=>x.Username).IsRequired().HasMaxLength(64);
            builder.Property(x=>x.PasswordHash).IsRequired();
            //builder.HasMany(x => x.Accounts).WithOne(x => x.User);
            builder.HasMany(x => x.Accounts).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
