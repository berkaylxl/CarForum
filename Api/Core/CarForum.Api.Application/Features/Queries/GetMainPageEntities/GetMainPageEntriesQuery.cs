using CarForum.Common.Models.Page;
using CarForum.Common.Models.Queries;
using MediatR;
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
}
