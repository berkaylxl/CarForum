using CarForum.Common;
using CarForum.Common.Events.EntryComment;
using CarForum.Common.Infrastructure;
using MediatR;

namespace CarForum.Api.Application.Features.Commands.EntryCommands.DeleteVote
{
    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand,bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.FavExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.DeleteEntryCommentVoteQueueName,
                obj: new DeleteEntryCommentVoteEvent()
                {
                    EntryCommentId=request.EntryCommentId,
                    CreatedBy=request.UserId
                 
                });
            return await Task.FromResult(true);
        }
    }
}
