using CarForum.Common;
using CarForum.Common.Events.EntryComment;
using CarForum.Common.Infrastructure;
using CarForum.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.EntryCommands.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.VoteExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.CreateEntryCommentVoteQueueName,
                obj: new CreateEntryCommentVoteEvent()
                {
                   EntryCommentId = request.EntryCommentId,
                   VoteType = request.VoteType,
                   CreatedBy=request.CreatedBy
                });
            return Task.FromResult(true);
        }
        
    }
}
