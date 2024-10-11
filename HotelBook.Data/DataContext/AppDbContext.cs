using Microsoft.EntityFrameworkCore;
using System.Reflection;
using HotelBook.Data.Entities;

namespace HotelBook.Data.DataContext
{
	public class AppDbContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Rent> Rent { get; set; }
		public DbSet<Room> Room { get; set; }
		public DbSet<RoomImage> RoomImages { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}