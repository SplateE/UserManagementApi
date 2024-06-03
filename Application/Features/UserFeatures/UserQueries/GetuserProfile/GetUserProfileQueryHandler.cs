using Application.IGenericInterfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.UserQueries.GetuserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ApplicationUser>
    {
        private readonly IUnitOfWork _uow;
        public GetUserProfileQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ApplicationUser> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Users.GetByIdAsync(request.id.ToString());
        }
    }
}
