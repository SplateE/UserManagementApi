using Application.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.UserQueries.LoginUser
{
    public class UserLoginQuery : IRequest<LoginResponse>
    {
        public string Email { get; set; }  
        public string Password { get; set; } 
    }
}
