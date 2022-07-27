using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarForum.Api.Application.Features.Queries.GetEntries
{
    public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
    {
        private readonly IEntryRepository _entryRepo;
        private readonly IMapper _mapper;

        public GetEntriesQueryHandler(IEntryRepository entryRepo, IMapper mapper)
        {
            _entryRepo = entryRepo;
            _mapper = mapper;
        }

        public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepo.AsQueryable();

            if (request.TodaysEntries)
            {
                query = query
                    .Where(i => i.CreateDate >= DateTime.Now.Date)
                    .Where(i => i.CreateDate <= DateTime.Now.AddDays(1).Date);
            }
            query = query.Include(i => i.EntryComments)
                .OrderBy(i => Guid.NewGuid())
                .Take(request.Count)
                ;
            return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
