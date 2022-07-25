using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Exceptions;
using MediatR;

namespace CarForum.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailConfirmationRepository _emailRepo;
        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await _emailRepo.GetByIdAsync(request.ConfirmationId);

            if (confirmation == null)
                throw new DatabaseValidationExcepton("Confirmation not found");
           
            var dbUser = await _userRepo.GetSingleAsync(i => i.EmailAdress == confirmation.NewEmailAdress);
           
            if (dbUser is null)
                throw new DatabaseValidationExcepton("user not found with this email");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationExcepton("Email adress is already confirmed");

            dbUser.EmailConfirmed = true;
            await _userRepo.UpdateAsync(dbUser); 
            return true;
        }

    }
}
