using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.UserFeatures.UserCommand.Registration
{
    public class RegisterUserCommand : IRequest<string>
    {
        public string? Name { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        public string? Password { get; set; } = string.Empty;
    }
}
