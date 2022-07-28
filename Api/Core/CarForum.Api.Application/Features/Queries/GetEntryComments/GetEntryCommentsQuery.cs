﻿using CarForum.Common.Models.Page;
using CarForum.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQuery : BasePageQuery,IRequest<PagedViewModel<GetEntryCommentsViewModel>>
    {
        public GetEntryCommentsQuery(Guid entryId, Guid? userId,int page, int pageSize) : base(page, pageSize)
        {
            EntryId = entryId;
            UserId = userId;
        }
        public Guid EntryId { get; set; }
        public Guid?  UserId { get; set; }
    }
}
