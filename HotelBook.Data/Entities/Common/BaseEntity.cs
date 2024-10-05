﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBook.Data.Entities.Common
{
	public abstract class BaseEntity
	{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
}