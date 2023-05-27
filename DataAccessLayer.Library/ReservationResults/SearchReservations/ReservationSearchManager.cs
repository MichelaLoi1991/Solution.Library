using DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch;
using DataAccessLayer.Library.Search;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults.SearchReservations
{
    public class ReservationSearchManager : IReservationSearchManager
    {
        IReservationSearch UserSearcher { get; set; }
        IReservationBookTitleSearch BookSearcher { get; set; }

        IReservationStatusSearch ReservationStatusSearcher { get; set; }

        // constructor
        public ReservationSearchManager(IReservationSearch userSearcher, IReservationBookTitleSearch bookSearcher, IReservationStatusSearch reservationStatusSearcher)
        {
            UserSearcher = userSearcher;
            BookSearcher = bookSearcher;
            ReservationStatusSearcher = reservationStatusSearcher;


        }
        public List<Reservation> Search(int? userId, Book book, bool? reservationStatus, List<Reservation> reservationList)
        {
            var userIdReservations = UserSearcher.Search(userId, reservationList);
            var bookReservations = BookSearcher.Search(book, userIdReservations);
            return ReservationStatusSearcher.Search(reservationStatus, bookReservations);
        }
    }
}
