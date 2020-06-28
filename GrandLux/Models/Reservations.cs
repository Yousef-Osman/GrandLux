using System;
using System.Collections.Generic;

namespace GrandLux.Models
{
    public partial class Reservations
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int Adults { get; set; }
        public int? Children { get; set; }
        public int? ReservationStatus { get; set; }

        public virtual Guests Guest { get; set; }
        public virtual ReservationStatus ReservationStatusNavigation { get; set; }
        public virtual Rooms Room { get; set; }
    }
}
