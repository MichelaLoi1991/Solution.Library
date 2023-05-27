using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Repository
{
    public interface IRepository
    {

        //Users CRUD
        User Login(string username, string password);

        //User ReadUserById(int id);

        User GetUserByUserName(string userName);


        // Books CRUD
        List<Book> ReadAllBooks();


        Book ReadBookByBookTitle(string bookTitle);

        string CreateBook(Book book);

        string UpdateBook(int bookId, Book bookWithNewValues);

        string DeleteBook(int bookId);
        
        // Search Book
        List<Book> SearchBook(Book book);

        // Reservations CRUD

        List<Reservation> ReadReservationByUserId(int id);
        List<Reservation> ReadAllReservations();

        string ReserveBook(int userId, int bookId);
        string BookReturn(int userId, int bookId);

        // Reservation Search
        List<Reservation> SearchReservationHistory(User user, Book book, bool? reservationStatus);

        Dictionary<Book, DateTime> SearchBookWithAvailabilityInfos(Book book);
    }
}
