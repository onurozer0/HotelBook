using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBook.Data.Entities.Common;

namespace HotelBook.Data.Entities
{
	public class Room : BaseEntity
	{
		public string RoomNumber { get; set; }
		public int BedCount { get; set; }
		public bool IsFreeWifiAccess { get; set; }
		public bool IsAvailaible { get; set; }
		public bool Price { get; set; }
		public RoomType Type { get; set; }
		public int MaxPerson { get; set; }
		public int Capacity { get; set; }
		public bool FreeBreakfast { get; set; }
		public Rent? Rent { get; set; }
	}
}