using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch
{
    public interface IReservationBookTitleSearch
    {
        List<Reservation> Search(Book book, List<Reservation> reservations);
    }
}
