using CarForum.Common;
using CarForum.Common.Events.Entry;
using CarForum.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.VoteExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.DeleteEntryVoteQueueName,
                obj: new DeleteEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy=request.UserId

                });
            return await Task.FromResult(true);
        }
    }
}
