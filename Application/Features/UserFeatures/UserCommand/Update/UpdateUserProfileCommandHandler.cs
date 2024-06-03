using Application.Features.UserFeatures.UserCommand.Registration;
using Application.IGenericInterfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.UserCommand.Update
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;

        public UpdateUserProfileCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            
            user.Name = request.Name;
            user.Email = request.Email;

            if (!string.IsNullOrEmpty(request.Password))
            {
                var passwordValidator = _userManager.PasswordValidators.FirstOrDefault();
                if (passwordValidator != null)
                {
                    var result = await passwordValidator.ValidateAsync(_userManager, user, request.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Invalid password.");
                    }
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
            }

         
            _uow.Users.Update(user);

            try
            {
                await _uow.SaveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("User update failed.", ex);
            }

            return true;
        }
    }
}