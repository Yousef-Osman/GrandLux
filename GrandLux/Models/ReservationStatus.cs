using System;
using System.Collections.Generic;

namespace GrandLux.Models
{
    public partial class ReservationStatus
    {
        public ReservationStatus()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
