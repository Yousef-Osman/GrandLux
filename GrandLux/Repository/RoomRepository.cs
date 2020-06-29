using GrandLux.Models;
using GrandLux.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly GrandLuxDBContext _context;

        public RoomRepository(GrandLuxDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Rooms> CheckAvailableRooms(DataViewModel data)
        {
            var capacity = data.Adults + data.Children;
            var roomList = _context.Rooms.ToList();
            var availableRooms = new List<Rooms>();

            foreach (var room in roomList)
            {
                var roomType = _context.RoomType.Where(t => t.Id == room.TypeId).SingleOrDefault();
                var roomStatus = _context.RoomStatus.Where(s => s.Id == room.StatusId).SingleOrDefault();

                bool inDate = data.CheckIn >= room.CheckIn && data.CheckIn <= room.CheckOut;
                bool outDate = data.CheckOut >= room.CheckIn && data.CheckOut <= room.CheckOut;
                bool availableCapacity = roomType.MaxCapacity >= capacity;
                bool availability = roomStatus.Name == "Available";
                if (!(inDate || outDate) && availableCapacity && availability)
                {
                    availableRooms.Add(room);
                }
            }

            return availableRooms;
        }

        public void UpdateRoom(DataViewModel data)
        {
            var room = _context.Rooms.Where(r => r.Id == data.roomId).SingleOrDefault();

            if(room != null)
            {
                room.CheckIn = data.CheckIn;
                room.CheckOut = data.CheckOut;

                try
                {
                    _context.Update(room);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
