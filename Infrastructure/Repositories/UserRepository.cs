using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Core;
using Application.Features.UserFeatures.UserQueries.LoginUser;
using Application.Features.UserFeatures.UserCommand.Registration;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUser
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext dbContext, IConfiguration configuration, UserManager<ApplicationUser> userManager) : base(dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task<LoginResponse> LoginUserAsync(UserLoginQuery loginRequest)
        {
            var GetUser = await FindUserByEmail(loginRequest.Email!);
            if (GetUser == null)
            {
                return new LoginResponse(false, "User Not Found");
            }
            else
            {

                bool CheckPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, GetUser.Password);
                if (CheckPassword)
                {
                    var userRoles = await _userManager.GetRolesAsync(GetUser);
                    return new LoginResponse(true, "Logged in Successfully", GenerateJWTToken(GetUser, userRoles));
                }
                else
                {
                    return new LoginResponse(false, "Invalid Credentials");
                }
            }

        }
        private string GenerateJWTToken(ApplicationUser UserInfo, IEnumerable<string> userRoles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,UserInfo.Id.ToString()),
                new Claim(ClaimTypes.Name,UserInfo.Name!),
                new Claim(ClaimTypes.Email,UserInfo.Email!),
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<RegisterResponse> RegisterUserAsync(RegisterUserCommand registerUserDTO)
        {
            var GetUser = await FindUserByEmail(registerUserDTO.Email);
            if (GetUser != null)
            {
                return new RegisterResponse(false, "User Already exists");
            }
            await _dbContext.ApplicationUser.AddAsync(new ApplicationUser
            {
                Email = registerUserDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
                Name = registerUserDTO.Name
            });

            return new RegisterResponse( true, "Registration Complete");
        }
        public async Task<ApplicationUser> FindUserByEmail(string email)
        {
            var ret =   _dbContext.ApplicationUser.AsNoTracking().FirstOrDefault<ApplicationUser>(u => u.Email == email);
            return ret;
        }
    }
}
