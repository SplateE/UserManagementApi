using Application.IGenericInterfaces;
using MediatR;

namespace Application.Features.UserFeatures.UserCommand.Registration
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUnitOfWork _uow;

        public RegisterUserCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _uow.Users.RegisterUserAsync(request);
            if (!result.IsSuccess)
            {
                throw new Exception(result.Message);
            }
            await _uow.SaveAsync(cancellationToken);
            var user = await _uow.Users.FindUserByEmail(request.Email);
            return user.Id.ToString();
        }
    }
}
