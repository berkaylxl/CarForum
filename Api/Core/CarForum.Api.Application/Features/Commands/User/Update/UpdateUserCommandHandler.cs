using AutoMapper;
using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common;
using CarForum.Common.Events.User;
using CarForum.Common.Exceptions;
using CarForum.Common.Infrastructure;
using CarForum.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);

            if (dbUser is null)
                throw new DatabaseValidationExcepton("User not found ");

            var dbEmailAdress = dbUser.EmailAdress;
            var emailChanged = string.CompareOrdinal(dbEmailAdress, request.EmailAdress) != 0;

            _mapper.Map(request, dbUser);

            var rows = await _userRepository.UpdateAsync(dbUser);

            if (emailChanged && rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAdress = null,
                    NewEmailAdress = dbUser.EmailAdress
                };
                QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.UserExchangeName,
                                                    exchangeType: CarForumConstants.DefaultExchangeType,
                                                    queueName: CarForumConstants.UserEmailChangedQueueName,
                                                    obj: @event);

                dbUser.EmailConfirmed = false;
                await _userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
