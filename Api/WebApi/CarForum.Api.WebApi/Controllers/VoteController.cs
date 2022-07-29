using CarForum.Api.Application.Features.Commands.Entry.DeleteVote;
using CarForum.Api.Application.Features.Commands.EntryCommands.DeleteVote;
using CarForum.Common.Models.RequestModels;
using CarForum.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarForum.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        
        private readonly IMediator _mediator;

        [HttpPost]
        [Route("Entry/{entryId}")]
        public  async Task<IActionResult> CreateEntryVote (Guid entryId,VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId.Value));
            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));
            return Ok();
        }
        [HttpPost]
        [Route("DeleteEntryCommentVote/{EntryId}")]
        public async Task<IActionResult>DeleteEntryCommentVote(Guid entryCommentId)
        {
            await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));
            return Ok();
        }

    }
}
