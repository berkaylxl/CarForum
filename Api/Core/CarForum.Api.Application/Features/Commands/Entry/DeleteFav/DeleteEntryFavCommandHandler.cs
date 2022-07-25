using CarForum.Common;
using CarForum.Common.Events.Entry;
using CarForum.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.FavExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.DeleteEntryFavQueueName,
                obj: new DeleteEntryFavEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy=request.UserId
                });
            return Task.FromResult(true);
        }
    }
}
