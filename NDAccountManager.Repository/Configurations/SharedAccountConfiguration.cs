using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDAccountManager.Core.Models;

namespace NDAccountManager.Repository.Configurations
{
    public class SharedAccountConfiguration : IEntityTypeConfiguration<SharedAccount>
    {
        public void Configure(EntityTypeBuilder<SharedAccount> builder)
        {
            builder.HasKey(x => new { x.UserId, x.AccountId });

            builder.HasOne(x => x.User)
                .WithMany(u => u.SharedAccounts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Account)
                .WithMany(a => a.SharedAccounts)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}