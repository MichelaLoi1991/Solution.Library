using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.ViewModels
{
    public class SearchReservationHistoryViewModel
    {
        public string BookTitle {get; set;}
        public string UserName { get; set; }
        public bool? ReservationStatus { get; set; } 
    }
}
