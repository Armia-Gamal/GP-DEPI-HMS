using Hospital.Services;
using Microsoft.AspNetCore.Mvc;
using cloudscribe.Pagination.Models;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Areas.Admin
{
    [Area("admin")]
    public class RoomsController : Controller
    {
        private IRoomService _room;
        private IHospitalInfo _hospitalInfo;

        public RoomsController(IRoomService room, IHospitalInfo hospitalInfo)
        {
            _room = room;
            _hospitalInfo = hospitalInfo;
        }

        // Action method for paginated room list
        [Route("Rooms")]
        [Route("Rooms/Index")]
        [Route("admin/Rooms")]
        [Route("admin/Rooms/Index")]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            // Ensure you're calling the correct service (_room instead of _contact)
            var pagedRooms = _room.GetAll(pageNumber, pageSize);
            return View(pagedRooms);  // Pass the paginated room data to the view
        }

        // Edit room GET method
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1, 10).Data, "Id", "Name");
            var viewModel = _room.GetRoomById(id);
            return View(viewModel);
        }

        // Edit room POST method
        [HttpPost]
        public IActionResult Edit(RoomViewModel vm)
        {
            _room.UpdateRoom(vm);
            return Redirect("/admin/Rooms/Index");
        }

        // Create room GET method
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1, 10).Data, "Id", "Name");
            return View();
        }

        // Create room POST method
        [HttpPost]
        public IActionResult Create(RoomViewModel vm)
        {
            _room.InsertRoom(vm);
            return Redirect("/admin/Rooms/Index");
        }

        // Delete room method
        public IActionResult Delete(int id)
        {
            _room.DeleteRoom(id);
            return Redirect("/admin/Rooms/Index");
        }
    }
}
