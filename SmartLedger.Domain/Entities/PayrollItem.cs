using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.Entities
{
    public class PayrollItem : BaseEntity.BaseEntity<int>
    {
        public string Type { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

    }
}
