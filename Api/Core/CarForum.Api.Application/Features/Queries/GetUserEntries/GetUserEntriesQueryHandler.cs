using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Common.Infrastructure.Extensions;
using CarForum.Common.Models.Page;
using CarForum.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepo;
        public GetUserEntriesQueryHandler(IEntryRepository entryRepo)
        {
            _entryRepo = entryRepo;
        }
        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepo.AsQueryable();
            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            {
                query = query.Where(i => i.CreatedById == request.UserId);

            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(i => i.CreatedBy.Username == request.UserName);
            }
            else return null;

            query = query.Include(i => i.EntryFavorites)
                        .Include(i => i.CreatedBy);
            var list = query.Select(i => new GetUserEntriesDetailViewModel()
            {
                Id=i.Id,
                Subject=i.Subject,
                Content=i.Content,
                IsFavorited=false,
                FavoritedCount=i.EntryFavorites.Count,
                CreatedDate=i.CreateDate,
                CreatedByUserName=i.CreatedBy.Username
            });
            var entries = await list.GetPaged(request.Page, request.PageSize);
            return new PagedViewModel<GetUserEntriesDetailViewModel>(entries.Results, entries.PageInfo);

        }
    }
}


