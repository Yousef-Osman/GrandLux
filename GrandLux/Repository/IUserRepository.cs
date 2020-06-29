using GrandLux.Data;
using GrandLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public interface IUserRepository
    {
        Guests AddUser(ApplicationUser user);
    }
}
