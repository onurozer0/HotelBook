using HotelBook.Data.DataContext;

namespace HotelBook.Data.UnitOfWorks;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}