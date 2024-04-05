using AutoMapper;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDesk.DAL.Entities;
using ServiceDesk.DAL.GenericRepository;

namespace ServiceDesk.BLL.Services;

public class UserService : GenericService<UserModel, User>, IUserService
{
    public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        : base(userRepository, mapper)
    {
    }
}
