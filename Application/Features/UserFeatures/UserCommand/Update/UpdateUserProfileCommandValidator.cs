using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.UserCommand.Update
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID Cannot be empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Cannot be empty").MinimumLength(2).WithMessage("Name must be at least 2 characters long");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Cannot be empty")
                                     .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                                     .WithMessage("Password must contain at least 1 uppercase letter, 1 digit, and 1 special character, and be at least 8 characters long");
        }
    }
}
