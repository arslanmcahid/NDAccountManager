using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NDAccountManager.Core.Models;
using NDAccountManager.Core.Repositories;
using NDAccountManager.Core.Services;
using NDAccountManager.Core.UnitOfWorks;

namespace NDAccountManager.Service.Services
{
    public class UserService : Service<User> , IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
    }
}
