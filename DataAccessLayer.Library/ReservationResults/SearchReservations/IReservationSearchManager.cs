using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults.SearchReservations
{
    public interface IReservationSearchManager
    {
        List<Reservation> Search(int? user, Book book, bool? reservationStatus, List<Reservation> reservationList);
    }
}
