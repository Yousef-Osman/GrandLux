using System;
using System.Collections.Generic;

namespace GrandLux.Models
{
    public partial class RoomStatus
    {
        public RoomStatus()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
