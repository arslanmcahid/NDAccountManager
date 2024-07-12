using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Core.DTOs
{
    public class UserDto:BaseDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
