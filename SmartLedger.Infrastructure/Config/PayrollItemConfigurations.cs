using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLedger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Infrastructure.Config
{
    internal class PayrollItemConfigurations : IEntityTypeConfiguration<PayrollItem>
    {
        public void Configure(EntityTypeBuilder<PayrollItem> builder)
        {
            builder.HasMany(p => p.Accounts)
                   .WithOne(a => a.PayrollItem)
                   .HasForeignKey(a => a.PayrollItemId);
        }
    }
}
