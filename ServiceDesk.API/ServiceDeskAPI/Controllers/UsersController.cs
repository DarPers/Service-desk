using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.BLL.Interfaces;
using ServiceDesk.BLL.Models;
using ServiceDeskAPI.Constants;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Delete user with certain id
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// DELETE /users/{id}
    /// </remarks>
    [HttpDelete(EndpointsConstants.RequestWithId)]
    public Task DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        return _userService.DeleteModelAsync(id, cancellationToken);
    }

    /// <summary>
    /// Create new user
    /// </summary>
    /// <param name="user">New user</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// POST /users
    /// {
    ///     "firstName": "Klaus",
    ///     "lastName": "Russel",
    ///     "email": "ron@email.com",
    ///     "password": "12345were67",
    ///     "role": "OperationAdministrator"
    /// }
    /// </remarks>
    [HttpPost]
    public async Task<UserViewModel> CreateUser(UserRegistrationViewModel user, CancellationToken cancellationToken)
    {
        var userModel = _mapper.Map<UserModel>(user);
        var newUserModel = await _userService.CreateModelAsync(userModel, cancellationToken);
        return _mapper.Map<UserViewModel>(newUserModel);
    }

    /// <summary>
    /// Update user with certain id
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// PUT /users
    /// {
    ///     "firstName": "Klause",
    ///     "lastName": "Russel",
    ///     "email": "ron@email.com",
    /// }
    /// </remarks>
    [HttpPut(EndpointsConstants.RequestWithId)]
    public async Task<UserViewModel> UpdateUser(Guid id, UserUpdatingViewModel user, CancellationToken cancellationToken)
    {
        var userModel = _mapper.Map<UserModel>(user);
        var newUserModel = await _userService.UpdateModelAsync(id, userModel, cancellationToken);
        return _mapper.Map<UserViewModel>(newUserModel);
    }

    /// <summary>
    /// Get user with certain id
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken"></param>
    /// <remarks>
    /// Sample request:
    /// GET /users/{id}
    /// </remarks>
    [HttpGet(EndpointsConstants.RequestWithId)]
    public async Task<UserViewModel> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var userModel = await _userService.GetModelByIdAsync(id, cancellationToken);
        return _mapper.Map<UserViewModel>(userModel);
    }
}
