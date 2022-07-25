using CarForum.Common;
using CarForum.Common.Events.EntryComment;
using CarForum.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.EntryCommands.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.FavExchangeName,
                     exchangeType: CarForumConstants.DefaultExchangeType,
                     queueName: CarForumConstants.CreateEntryCommentFavQueueName,
                     obj: new CreateEntryCommentFavEvent()
                     {
                         EntryCommentId = request.EntryCommentId,
                         CreatedBy=request.UserId
                     });
            return await Task.FromResult(true);
        }
    }
}
