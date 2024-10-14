using HotelBook.Data.DataContext;
using HotelBook.Data.Entities;
using HotelBook.Data.Repositories.GenericRepositories;
using System.Linq.Expressions;
using System.Net.Http.Headers;


namespace HotelBook.Data.Repositories.RoomRepositories
{
    public class RoomRepositories : IRoomRepositories
    {
        private readonly IGenericRepository<Room> _repository;
        private readonly AppDbContext _appDbContext;

        public RoomRepositories(IGenericRepository<Room> repository, AppDbContext appDbContext)
        {
            _repository = repository;
            _appDbContext = appDbContext;
        }

        public async Task<Room> CreateAsync(Room room)
        {
            return await _repository.CreateAsync(room);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> IsEntityUpdateableAsync(int id)
        {
            return await _repository.IsEntityUpdateableAsync(id);
        }

        public async Task<Room> UpdateAsync(Room room)
        {
            return await _repository.UpdateAsync(room);
        }

        public async Task<IQueryable<Room>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }

        public IQueryable<Room> GetBy(Expression<Func<Room, bool>> predicate)
        {
            return _repository.GetBy(predicate);
        }

        public async Task<IQueryable<Room>> GetByActiveStatementAsync(bool isActive)
        {
            return await _repository.GetByActiveStatementAsync(isActive);
        }

        public async Task<IQueryable<Room>> GetByDeleteStatementAsync(bool isDeleted)
        {
            return await _repository.GetByDeleteStatementAsync(isDeleted);
        }

        public async Task<(int, int, IQueryable<Room>)> GetPagedAsync(int pageNumber, int pageSize, bool? isActive, params Expression<Func<Room, object>>[] additionalFilters)
        {
            return await _repository.GetPagedAsync(pageNumber, pageSize, isActive, additionalFilters);
        }

    }
}
