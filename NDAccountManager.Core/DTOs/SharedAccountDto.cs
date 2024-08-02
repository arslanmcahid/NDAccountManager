using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Core.DTOs
{
    public class SharedAccountDto
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public bool IsUnlimited { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
