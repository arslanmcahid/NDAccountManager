using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDAccountManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Repository.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).UseIdentityColumn();
            builder.Property(x => x.Platform).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Username).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x=>x.IPAddress).IsRequired().HasMaxLength(32);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(64);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.User).IsRequired();
        }
    }
}