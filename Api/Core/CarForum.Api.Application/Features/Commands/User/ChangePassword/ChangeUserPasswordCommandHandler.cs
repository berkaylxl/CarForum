using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Events.User;
using CarForum.Common.Exceptions;
using CarForum.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }
            var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationExcepton("user not found");
            var encrypPassword = PasswordEncryptor.Encrpt(request.OldPassword);
            if (dbUser.Password != encrypPassword)
                throw new DatabaseValidationExcepton("Old password wrong!");
            
            dbUser.Password = encrypPassword;
            await _userRepository.UpdateAsync(dbUser);
            return true;
        
        
        }
    }
}
