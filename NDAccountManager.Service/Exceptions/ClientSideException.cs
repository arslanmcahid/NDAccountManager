﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Service.Exceptions
{
    public class ClientSideException:Exception
    {
        public ClientSideException(string message) : base(message)
        {
            
        }
    }
}
