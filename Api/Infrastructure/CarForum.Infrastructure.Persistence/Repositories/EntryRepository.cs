using CarForum.Api.Application.Interfaces.Repositories;
using CarForum.Api.Domain.Models;
using CarForum.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Infrastructure.Persistence.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        public EntryRepository(CarForumContext dbContext) : base(dbContext)
        {
        }
    }
}
