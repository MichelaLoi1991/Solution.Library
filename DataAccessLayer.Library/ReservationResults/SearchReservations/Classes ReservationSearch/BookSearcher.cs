using DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch;
using DocumentFormat.OpenXml.Spreadsheet;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults.SearchReservations.Classes_ReservationSearch
{
    public class BookSearcher : IReservationBookTitleSearch
    {
        public List<Reservation> Search(Book book, List<Reservation> reservations)
        {
            if (!string.IsNullOrEmpty(book.Title))
            {
                return reservations.Where(r => r.Book.Title.ToLower().Contains(book.Title.ToLower())).Distinct().
                ToList();
            }
            else
            {
                return reservations;
            }
        }
    }
}
