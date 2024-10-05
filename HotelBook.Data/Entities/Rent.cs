using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBook.Data.Entities
{
	public class Rent
	{
		public int Id { get; set; }
		public int PersonCount { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int RoomId { get; set; }
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public Room Room { get; set; }
	}
}