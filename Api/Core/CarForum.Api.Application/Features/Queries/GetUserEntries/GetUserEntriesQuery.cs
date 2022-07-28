using CarForum.Common.Models.Page;
using CarForum.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasePageQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public GetUserEntriesQuery(Guid? userId, string userName = null, int page=1, int pageSize=10) : base(page, pageSize)
        {
            UserName = userName;
            UserId = userId;
        }

        public Guid? UserId { get; set; }
        public string UserName { get; set; }

    }
}
