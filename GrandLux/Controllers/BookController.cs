using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandLux.Data;
using GrandLux.Repository;
using GrandLux.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandLux.Controllers
{
    public class BookController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public BookController(UserManager<ApplicationUser> userManager,
                              IUserRepository userRepository,
                              IRoomRepository roomRepository,
                              IReservationRepository reservationRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckAvailability(DataViewModel data)
        {
            data.rooms = _roomRepository.CheckAvailableRooms(data).ToList();
            return PartialView("_AvailableRooms", data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReservation(DataViewModel data)
        {
            var user = await (_userManager.GetUserAsync(User));
            data.guest = _userRepository.AddUser(user);
            _reservationRepository.AddReservation(data);
            _roomRepository.UpdateRoom(data);

            return PartialView("_Success");
        }
    }
}
