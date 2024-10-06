using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBook.Data.Entities.Common;

namespace HotelBook.Data.Entities
{
	public class Customer : BaseEntity
	{
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public Gender Gender { get; set; }	
		public Rent? Rent { get; set; }
	}
}