using Domain.Entities;
using MediatR;

namespace Application.Features.UserFeatures.UserQueries.GetuserProfile
{
    public class GetUserProfileQuery : IRequest<ApplicationUser>
    {
        public Guid id { get; set; }
    }
}