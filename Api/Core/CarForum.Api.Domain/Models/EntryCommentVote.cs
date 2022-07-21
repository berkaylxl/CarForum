using CarForum.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Domain.Models
{
    public class EntryCommentVote:BaseEntity
    {
        public VoteType VoteType { get; set; }
        public Guid EntryCommentId { get; set; }
        public Guid CreatedById { get; set; }
        public virtual EntryComment EntryComment { get; set; }
    }
}
