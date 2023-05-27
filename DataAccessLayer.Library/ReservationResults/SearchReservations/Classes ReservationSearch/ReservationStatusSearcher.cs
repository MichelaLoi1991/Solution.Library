using DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults.SearchReservations.Classes_ReservationSearch
{
    public class ReservationStatusSearcher : IReservationStatusSearch
    {
        public List<Reservation> Search(bool? boolToSearch, List<Reservation> reservations)
        {
            if (boolToSearch == false)
            {
                return reservations.Where(r => r.EndDate <= DateTime.Today).Distinct().ToList();
            }
            else if(boolToSearch == true)
            {
                return reservations.Where(r => r.EndDate > DateTime.Today).Distinct().ToList();
            }
            else
            {
                return reservations;
            }
        }
    }
}
