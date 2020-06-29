using GrandLux.Models;
using GrandLux.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly GrandLuxDBContext _context;
        private readonly Reservations reservation;

        public ReservationRepository(GrandLuxDBContext context)
        {
            _context = context;
            reservation = new Reservations();
        }

        public void AddReservation(DataViewModel data)
        {
            reservation.CheckIn = data.CheckIn;
            reservation.CheckOut = data.CheckOut;
            reservation.Adults = data.Adults;
            reservation.Children = data.Children;
            reservation.GuestId = data.guest.Id;
            reservation.RoomId = data.roomId;

            try
            {
                _context.Add(reservation);
                _context.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
    }
}
