namespace HotelBook.Data.UnitOfWorks;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    void SaveChanges();
}