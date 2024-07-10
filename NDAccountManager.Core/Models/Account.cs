using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Core.Models
{
    public class Account : BaseEntity
    {
        public string Platform { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
