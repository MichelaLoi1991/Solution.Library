using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.ViewModels
{
    public class ReservationViewModel
    {
        public UserViewModel User { get; set; }
        public DateTime EndDate {get; set;}
        public bool ReservationStatus { get; set; }
        public BookViewModel Book { get; set; }


    }
}
