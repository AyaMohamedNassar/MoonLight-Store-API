using Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class EmailConfirmationTokenConfiguration : IEntityTypeConfiguration<EmailConfirmationToken>
    {
        public void Configure(EntityTypeBuilder<EmailConfirmationToken> builder)
        {
            builder.HasOne(eConToken => eConToken.User).WithMany()
                .HasForeignKey(eConToken => eConToken.UserId);
        }
    }
}
