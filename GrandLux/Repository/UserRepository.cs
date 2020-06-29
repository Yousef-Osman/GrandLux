using GrandLux.Data;
using GrandLux.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GrandLuxDBContext _context;
        private readonly Guests guest;

        public UserRepository(GrandLuxDBContext context)
        {
            _context = context;
            guest = new Guests();
        }

        public Guests AddUser(ApplicationUser user)
        {
            var newGuest = _context.Guests.Where(g => g.EMail == user.Email).FirstOrDefault();

            if (newGuest == null)
            {
                guest.FirstName = user.FirstName;
                guest.LastName = user.LastName;
                guest.EMail = user.Email;
                guest.Phone = user.PhoneNumber;
                guest.Address = user.Address;
                guest.FirstName = user.FirstName;

                try
                {
                    _context.Add(guest);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                }
                
                newGuest = _context.Guests.Where(g => g.EMail == user.Email).SingleOrDefault();
            }

            return newGuest;
        }
    }
}
