using CarForum.Common;
using CarForum.Common.Events.Entry;
using CarForum.Common.Infrastructure;
using CarForum.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        public Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.VoteExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.CreateEntryVoteQueueName,
                obj: new CreateEntryVoteEvent() 
                {
                    EntryId=request.EntryId,
                    CreateBy=request.CreatedBy,
                    VoteType=request.VoteType,
                });
            return Task.FromResult(true);
        }
    }
}
