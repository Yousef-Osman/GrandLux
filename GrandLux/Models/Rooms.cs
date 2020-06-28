using System;
using System.Collections.Generic;

namespace GrandLux.Models
{
    public partial class Rooms
    {
        public Rooms()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public virtual RoomStatus Status { get; set; }
        public virtual RoomType Type { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
