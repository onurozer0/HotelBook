using HotelBook.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBook.Data.Entities
{
    public class RoomImage : BaseEntity
    {
        public int Id { get; set; }

        [Length(1, 100)]
        [Column(TypeName = "nvarchar(100)")]
        public string ImagePath { get; set; }

        [Length(1, 100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Text { get; set; } 
        public DateTime UploadDate { get; set; }  
        public int RoomId { get; set; } 
        public bool IsMainImage { get; set; } 
        public bool IsActive { get; set; }     
        public Room? Room { get; set; }  
    }
}
