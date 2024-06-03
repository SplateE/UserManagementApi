using Application.DTOs;
using Application.Features.UserFeatures.UserQueries.GetuserProfile;
using Application.IGenericInterfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.UserQueries.LoginUser
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, LoginResponse>
    {
        private readonly IUnitOfWork _uow;
        public UserLoginQueryHandler(IUnitOfWork uow) 
        {
            _uow = uow;
        }


        public async Task<LoginResponse> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        { 
            var result = await _uow.Users.LoginUserAsync(request);
            return result;
        }


    }
}
