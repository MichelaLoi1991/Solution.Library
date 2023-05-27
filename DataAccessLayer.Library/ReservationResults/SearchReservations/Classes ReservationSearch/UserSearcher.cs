using DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch;
using DocumentFormat.OpenXml.Spreadsheet;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults
{
    public class UserSearcher : IReservationSearch
    {
        public List<Reservation> Search(int? idToSearch, List<Reservation> reservations)
        {
            
            if (idToSearch == null || idToSearch == 0)
            {
                return reservations;
            }
            else
            {
                return reservations.Where(r => r.User.UserId == idToSearch).Distinct().ToList();
            }
            
        }
    }
}
