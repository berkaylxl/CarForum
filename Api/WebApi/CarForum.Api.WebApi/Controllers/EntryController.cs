using CarForum.Api.Application.Features.Queries.GetEntries;
using CarForum.Api.Application.Features.Queries.GetEntryComments;
using CarForum.Api.Application.Features.Queries.GetEntryDetail;
using CarForum.Api.Application.Features.Queries.GetMainPageEntities;
using CarForum.Api.Application.Features.Queries.GetUserEntries;
using CarForum.Common.Models.Queries;
using CarForum.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarForum.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;

        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var results = await _mediator.Send(new GetEntryDetailQuery(id,UserId));
            return Ok(results);
        }
        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id,int page,int pageSize)
        {
            var results = await _mediator.Send(new GetEntryCommentsQuery(id, UserId,page,pageSize));
            return Ok(results);
        }
        [HttpGet]
        [Route("UserEntries")]
        public async Task<IActionResult> GetUserEntries(string userName,Guid userId,int page,int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var results = await _mediator.Send(new GetUserEntriesQuery(userId,userName,page,pageSize));
            return Ok(results);
        }
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery]GetEntriesQuery query)
        {
            var entries = await _mediator.Send(query);
            return Ok(entries);
        }
        [HttpGet]
        [Route("MainPageEntires")]
        public async Task<IActionResult> MainPageEntires(int page ,int pageSize)
        {
            var entries = await _mediator.Send(new GetMainPageEntriesQuery(UserId,page,pageSize));
            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult>CreateEntry([FromBody]CreateEntryCommand command)
        {
            if (!command.CreateById.HasValue)
                command.CreateById = UserId;

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
        var result =await _mediator.Send(query);
            return Ok(result);
        }
    }
}
