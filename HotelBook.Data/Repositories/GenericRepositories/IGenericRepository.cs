using HotelBook.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBook.Data.Repositories.GenericRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<(int, int, IQueryable<T>)> GetPagedAsync(int pageNumber, int pageSize, bool? isActive, params Expression<Func<T, object>>[] additionalFilters);
        Task<bool> IsEntityUpdateableAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id); 
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetByActiveStatementAsync(bool isActive);
        Task<IQueryable<T>> GetByDeleteStatementAsync(bool isDeleted);


    }
}
