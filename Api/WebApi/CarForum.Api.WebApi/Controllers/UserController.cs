using CarForum.Api.Application.Features.Commands.User.ConfirmEmail;
using CarForum.Api.Application.Features.Queries.GetEntryComments;
using CarForum.Api.Application.Features.Queries.GetUserDetail;
using CarForum.Common.Events.User;
using CarForum.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarForum.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = _mediator.Send(new GetUserDetailQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [Route("UserName/{userName}")]
        public async Task<IActionResult>GetByUserName(string userName)
        {
            var result = await _mediator.Send(new GetUserDetailQuery( Guid.Empty,userName));
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var guid=await _mediator.Send(command);
            return Ok(guid);
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var guid=await _mediator.Send(command);
            return Ok(guid);
        }
        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var guid = await _mediator.Send(new ConfirmEmailCommand()
            {
                ConfirmationId = id
            }); 
            return Ok(guid);
        }
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;
            var guid = await _mediator.Send(command);
          
            return Ok(guid);
        }

    }
}
 