using CarForum.Common;
using CarForum.Common.Events.Entry;
using CarForum.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: CarForumConstants.FavExchangeName,
                exchangeType: CarForumConstants.DefaultExchangeType,
                queueName: CarForumConstants.CreateEntryFavQueueName,
                obj: new CreateEntryFavEvent()
                {
                    EntryId = request.EntryId.Value,
                    CreatedBy=request.UserId.Value
                });
            return Task.FromResult(true);
        }
    }
}
