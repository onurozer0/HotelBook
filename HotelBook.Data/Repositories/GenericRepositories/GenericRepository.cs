using HotelBook.Data.DataContext;
using HotelBook.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelBook.Data.Repositories.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
           
           entity.IsActive = true;
           var result = await _dbSet.AddAsync(entity);
           if(result.State == EntityState.Added)
           {
              return null;
           }
           return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null && !entity.IsDeleted)
            {
                entity.UpdatedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                entity.IsDeleted = true;
                entity.IsActive = false;
                return true;
            }
            return false;
        }
        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                var oldEntity = await _dbSet.AsNoTracking()
                    .Where(x => x.Id == entity.Id)
                    .FirstOrDefaultAsync();

                if (oldEntity != null)
                {
                   
                    entry.Property(e => e.IsDeleted).CurrentValue = oldEntity.IsDeleted;
                    var properties = typeof(T).GetProperties();

                    foreach (var property in properties)
                    {
                        if (property.CanWrite && property.CanRead)
                        {
                            var newValue = property.GetValue(entity, null);
                            var oldValue = property.GetValue(oldEntity, null);

                            if (newValue != null && oldValue == null || newValue != null && !newValue.Equals(oldValue))
                            {
                                _context.Entry(oldEntity).Property(property.Name).IsModified = true;
                                _context.Entry(oldEntity).Property(property.Name).CurrentValue = newValue;
                            }
                        }
                    }

                    entity = oldEntity;
                }
                else
                {                  
                    _dbSet.Add(entity);
                }
            }
            return entity;
        }
    

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet.AsNoTracking().AsQueryable());
        }

        public  IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<IQueryable<T>> GetByActiveStatementAsync(bool isActive)
        {
           var query = GetBy(e => e.IsActive ==  isActive && !e.IsDeleted).AsNoTracking();
            return await Task.FromResult(query);
        }

        public async Task<IQueryable<T>> GetByDeleteStatementAsync(bool isDeleted)
        {
            var query = GetBy(e => e.IsDeleted == isDeleted).AsNoTracking();
            return await Task.FromResult(query);   
        }

        public async Task<(int, int, IQueryable<T>)> GetPagedAsync(int pageNumber, int pageSize, bool? isActive, params Expression<Func<T, object>>[] additionalFilters)
        {
            var query = _dbSet.Where(x => !x.IsDeleted && x.IsActive == isActive);

            // Apply additional filters if provided
            foreach (var filter in additionalFilters)
            {
                query = query.Include(filter);
            }

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (pageNumber < 1 || pageNumber > totalPages)
            {
                return (0, 0, Enumerable.Empty<T>().AsQueryable());
            }

            var paged = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (totalPages, totalCount, paged.AsQueryable());
        }

    }
}
