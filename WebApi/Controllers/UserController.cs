using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Application.Features.UserFeatures.UserQueries.LoginUser;
using Application.Features.UserFeatures.UserQueries.GetuserProfile;
using Application.Features.UserFeatures.UserCommand.Registration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Application.Features.UserFeatures.UserCommand.Update;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginUser(LoginDTO LoginRequest)
        {
            var result = await _mediator.Send(new UserLoginQuery { Email = LoginRequest.Email, Password = LoginRequest.Password });
            return Ok(result);
        }
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<string>> RegisterUser(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken = default)
        {
            var Result = await _mediator.Send(new RegisterUserCommand { Email = registerUserDTO.Email, Name = registerUserDTO.Name, Password = registerUserDTO.Password }, cancellationToken);
            return Ok(Result);
        }
        [HttpGet("GetUserProfile")]
        [Authorize(Roles = "User,Admin")]//User&admin
        public async Task<ActionResult<ApplicationUser>> GetUserProfile(Guid UserId)
        {
            var result = await _mediator.Send(new GetUserProfileQuery { id = UserId });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("UpdateUserProfile")]
        [Authorize(Roles = "Admin")] //admin
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command
            , cancellationToken);
            return Ok();
        }
    }
}
