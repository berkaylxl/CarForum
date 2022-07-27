using AutoMapper;
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

namespace CarForum.Api.Application.Features.Queries.GetMainPageEntities
{
    public class GetMainPageEntriesQuery : BasePageQuery, IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {

        public GetMainPageEntriesQuery(Guid? userId, int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
        }

        public Guid? UserId { get; set; }
    }
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepo;
        private readonly IMapper _mapper;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepo, IMapper mapper)
        {
            _entryRepo = entryRepo;
            _mapper = mapper;
        }
        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepo.AsQueryable();
            query = query.Include(i => i.EntryFavorites)
                         .Include(i => i.CreatedBy)
                         .Include(i => i.EntryVotes);
            var list = query.Select(i => new GetEntryDetailViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryFavorites.Count,
                CreatedDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.Username,
                VoteType = request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                : Common.ViewModels.VoteType.None

            });
            var entries = await list.GetPaged(request.Page, request.PageSize);
            
            return new PagedViewModel<GetEntryDetailViewModel>(entries.Results, entries.PageInfo);


        }
    }
}
