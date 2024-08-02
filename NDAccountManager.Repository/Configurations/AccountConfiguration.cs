using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDAccountManager.Core.Models;

namespace NDAccountManager.Repository.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Platform).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Notes).IsRequired();
            builder.Property(x => x.OwnerId).IsRequired();
            builder.HasOne(x => x.Owner)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}