using AutoMapper;
using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Commands.EntryCommands.Create
{
    public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
    {
        private readonly IEntryCommentRepository _entryCommentCommand;
        private readonly IMapper _mapper;

        public CreateEntryCommentCommandHandler(IEntryCommentRepository entryCommentCommand, IMapper mapper)
        {
            _entryCommentCommand = entryCommentCommand;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
        {
            var dbEntryComment = _mapper.Map<Domain.Models.EntryComment>(request);
            await _entryCommentCommand.AddAsync(dbEntryComment);
            return dbEntryComment.Id;
        }
    }
}
