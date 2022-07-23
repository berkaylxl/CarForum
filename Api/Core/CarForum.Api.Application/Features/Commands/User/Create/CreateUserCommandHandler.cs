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

namespace CarForum.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var exitsUser = await _userRepository.GetSingleAsync(i => i.EmailAdress == request.EmailAdress);
            if(exitsUser is not null)
                throw new DatabaseValidationExcepton("User already exits");


            var dbUser = _mapper.Map<Domain.Models.User>(request);
            var rows = await _userRepository.AddAsync(dbUser);

            if(rows>0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAdress = null,
                    NewEmailAdress = dbUser.EmailAdress
                };
                QueueFactory.SendMessageToExchange(exchangeName:CarForumConstants.UserExchangeName,
                                                    exchangeType: CarForumConstants.DefaultExchangeType,
                                                    queueName: CarForumConstants.UserEmailChangedQueueName,
                                                    obj:@event);


            }
            return dbUser.Id;
         }
    }
}
