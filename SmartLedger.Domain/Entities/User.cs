using SmartLedger.Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLedger.Domain.Entities
{
    public class User:BaseEntity.BaseEntity<int>
    {
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Admin / User
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }

}
