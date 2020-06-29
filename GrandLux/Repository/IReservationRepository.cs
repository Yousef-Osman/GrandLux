using GrandLux.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandLux.Repository
{
    public interface IReservationRepository
    {
        void AddReservation(DataViewModel data);
    }
}
