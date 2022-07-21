using CarForum.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Api.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> AddAsync(TEntity user);
        int Add(TEntity entity);
        int Add(IEnumerable<TEntity> entities);

        Task<int> UpdateAsync(TEntity user);
        int Update(TEntity entity);


        Task<int> DeleteAsync(Guid id);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        int Delete(Guid guid);

        Task<bool> DeleteRAngeAsync(Expression<Func<TEntity, bool>> predicate);
       


    }
}
