using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ContactsController : Controller
    {
        private IContactService _contact;
        private IHospitalInfo _hospitalInfo;

        public ContactsController(IContactService contact, IHospitalInfo hospitalInfo)
        {
            _contact = contact;
            _hospitalInfo = hospitalInfo;
        }


        [Route("Contacts")]
        [Route("Contacts/Index")]
        [Route("admin/Contacts")]
        [Route("admin/Contacts/Index")]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            // Ensure you're calling the correct service (_room instead of _contact)
            var pagedRooms = _contact.GetAll(pageNumber, pageSize);
            return View(pagedRooms);  // Pass the paginated room data to the view
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1,10).Data, "Id", "Name");
            var viewModel = _contact.GetContactById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ContactViewModel vm)
        {
            _contact.UpdateContact(vm);
            return Redirect("/admin/Contacts/Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.hospital = new SelectList(_hospitalInfo.GetAll(1,10).Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactViewModel vm)
        {
            _contact.InsertContact(vm);
            return Redirect("/admin/Contacts/Index");

        }

        public IActionResult Delete(int id)
        {
            _contact.DeleteContact(id);
            return Redirect("/admin/Contacts/Index");
        }
    }
}

