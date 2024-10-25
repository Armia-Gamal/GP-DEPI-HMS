using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Hospital.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room Number is required.")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
        public HospitalInfo HospitalInfo { get; set; }
        public int HospitalInfoId { get; set; }


        public RoomViewModel() { 
        }

        public RoomViewModel(Room model)
        {
            Id = model.Id;
            Type = model.Type;
            Status = model.Status;
            RoomNumber = model.RoomNumber;
            HospitalInfoId = model.HospitalId;
            HospitalInfo = model.Hospital;
        }
        // The ConvertViewModel method takes a RoomViewModel object and converts it back to a Room model object. This is useful for mapping data from the ViewModel back to the Model, typically when saving changes to a database.
        public Room ConvertViewModel(RoomViewModel model)
        {
            return new Room
            {
                Id = model.Id,
                Type = model.Type,
                Status = model.Status,
                RoomNumber = model.RoomNumber,
                HospitalId = model.HospitalInfoId,
                Hospital = model.HospitalInfo
            };
        }
    }
}
