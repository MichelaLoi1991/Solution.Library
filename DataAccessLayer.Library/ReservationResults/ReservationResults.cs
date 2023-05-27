using DataAccessLayer.Library.ReservationResults.SearchReservations;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.ReservationResults
{
    public class ReservationResults : IReservationResults 
    {
        // proprieties
        public IBookDAO BookDAO { get; set; }
        public IReservationDAO ReservationDAO { get; set; }
        public IReservationSearchManager ReservationSearcherManager { get; set; }

        // constructor
        public ReservationResults(IReservationDAO reservationDAO, IBookDAO bookDAO, IReservationSearchManager reservationSearcherManager)
        {
            BookDAO = bookDAO;
            ReservationDAO = reservationDAO;
            ReservationSearcherManager = reservationSearcherManager;
        }

        // methods
        public void ReserveBook(User user, Book book)
        {
            DateTime startDate = DateTime.Today;
            DateTime endDate = startDate.AddDays(30);
            
            Reservation newReservation = new Reservation(0, user, book, startDate, endDate);
            ReservationDAO.Create(newReservation);
        }

        public string BookReturn(int user, int book)
        {
            List<Reservation> allReservations = ReservationDAO.ReadAll();
            var reservationSeleted = allReservations.Where(res => res.User.UserId == user && res.Book.BookId == book).SingleOrDefault();
            if(reservationSeleted.EndDate >= DateTime.Today) 
            { 
                ReservationDAO.Delete(reservationSeleted);
                return "Book was returned successfulllly";
            }
            else 
            {
                return "The Book " + reservationSeleted.Book.Title + " isn't active anymore";
            }
        }

        public List<Reservation> SearchReservationHistory(int? userId, Book book, bool? reservationStatus, List<Reservation> reservations) 
        {
            return ReservationSearcherManager.Search(userId, book, reservationStatus, reservations);
        }
    }
}
