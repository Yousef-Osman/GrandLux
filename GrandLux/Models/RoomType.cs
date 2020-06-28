using System;
using System.Collections.Generic;

namespace GrandLux.Models
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Rooms>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int NoOfBeds { get; set; }
        public int MaxCapacity { get; set; }

        public virtual ICollection<Rooms> Rooms { get; set; }
    }
}
