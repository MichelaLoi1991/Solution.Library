using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults
{
    public interface IReservationResults
    {
        void ReserveBook(User user, Book book);

        string BookReturn(int user, int book);

        List<Reservation> SearchReservationHistory(int? userId, Book book, bool? reservationStatus, List<Reservation> reservations);
    }
}
