using Application.DTOs;
using Application.Features.UserFeatures.UserCommand.Registration;
using Application.Features.UserFeatures.UserQueries.LoginUser;
using Application.IGenericInterfaces;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUser : IGenericRepository<ApplicationUser>
    {
        Task<RegisterResponse> RegisterUserAsync(RegisterUserCommand registerUserDTO);
        Task<LoginResponse> LoginUserAsync(UserLoginQuery loginDTO);
        Task<ApplicationUser> FindUserByEmail(string email);
    }
}
