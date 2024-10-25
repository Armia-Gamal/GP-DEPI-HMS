using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin
{
    [Area("admin")]
    public class HospitalsController : Controller
    {
        private IHospitalInfo _hospitalInfo;  // holds reference to (IHospitalInfo) to interact with hospital data.

        public HospitalsController(IHospitalInfo hospitalInfo)
		{
			_hospitalInfo = hospitalInfo;
		}

        [Route("Hospitals")]
        [Route("Hospitals/Index")]
        [Route("admin/Hospitals")]
        [Route("admin/Hospitals/Index")]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            // Ensure you're calling the correct service (_room instead of _contact)
            var pagedRooms = _hospitalInfo.GetAll(pageNumber, pageSize);
            return View(pagedRooms);  // Pass the paginated room data to the view
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _hospitalInfo.GetHospitalById(id);
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(HospitalInfoViewModel vm)
        {
            _hospitalInfo.UpdateHospitalInfo(vm);
            return Redirect("/admin/Hospitals/Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(HospitalInfoViewModel vm)
        {
            _hospitalInfo.InsertHospitalInfo(vm);
            return Redirect("/admin/Hospitals/Index");
        }

        public IActionResult Delete(int id)
        {
            _hospitalInfo.DeleteHospitalInfo(id);
            return Redirect("/admin/Hospitals/Index");
        }

    }
}
