﻿
namespace CarForum.WebApp.Infrastructure.Interfaces
{
    public interface IVoteService
    {
        Task CreateEntryCommentDownVote(Guid entryCommentId);
        Task CreateEntryCommentUpVote(Guid entryCommentId);
        Task CreateEntryDownVote(Guid entryId);
        Task CreateEntryUpVote(Guid entryId);
        Task DeleteEntryCommentVote(Guid entryCommentId);
        Task DeleteEntryVote(Guid entryId);
    }
}