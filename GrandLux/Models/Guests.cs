using System;
using System.Collections.Generic;

namespace GrandLux.Models
{
    public partial class Guests
    {
        public Guests()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
