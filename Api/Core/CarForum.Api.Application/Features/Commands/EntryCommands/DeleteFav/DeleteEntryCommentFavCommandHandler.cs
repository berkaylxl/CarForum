using CarForum.Common;
using CarForum.Common.Events.EntryComment;
using CarForum.Common.Infrastructure;
using MediatR;

namespace CarForum.Api.Application.Features.Commands.EntryCommands.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.FavExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.DeleteEntryCommentFavQueueName,
                obj: new DeleteEntryCommentFavEvent()
                {
                    EntryCommentId=request.EntryCommentId,
                    CreatedBy=request.UserId

                });
            return await Task.FromResult(true);
        }
    }
}
