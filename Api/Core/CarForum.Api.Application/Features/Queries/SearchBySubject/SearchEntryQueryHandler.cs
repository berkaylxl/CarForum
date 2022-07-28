
using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        private readonly IEntryRepository _entryRepo;

        public SearchEntryQueryHandler(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            var result = _entryRepo
                .Get(i => EF.Functions.Like(i.Subject, $"{request.Searchtext}%"))
                .Select(i => new SearchEntryViewModel()
                {
                    Id = i.Id,
                    Subject = i.Subject
                });
            return await result.ToListAsync(cancellationToken);
        }
    }
}
