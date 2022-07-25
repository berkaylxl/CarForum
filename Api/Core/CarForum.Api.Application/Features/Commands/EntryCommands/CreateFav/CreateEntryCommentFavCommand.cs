using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.EntryCommands.CreateFav
{
    public class CreateEntryCommentFavCommand:IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }

        public CreateEntryCommentFavCommand(Guid userId, Guid commentId)
        {
            EntryCommentId = commentId;
            UserId = userId;
            
        }
    }
}
