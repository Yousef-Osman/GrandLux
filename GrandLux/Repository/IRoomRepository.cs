using GrandLux.Models;
using GrandLux.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<Rooms> CheckAvailableRooms(DataViewModel data);
        void UpdateRoom(DataViewModel data);
    }
}
