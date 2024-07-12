using AutoMapper;
using NDAccountManager.Core.DTOs;
using NDAccountManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDAccountManager.Service.Mapping
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
