using GrandLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.ViewModels
{
    public class DataViewModel
    {
        public List<Rooms> rooms { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guests guest { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int roomId { get; set; }
    }
}
