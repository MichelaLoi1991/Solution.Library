using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch
{
    public interface IReservationSearch
    {
        List<Reservation> Search(int? idToSearch, List<Reservation> reservations);
    }
}
