using DataAccessLayer.Library.ReservationResults;
using DataAccessLayer.Library.Search;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using DocumentFormat.OpenXml.Wordprocessing;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Repository
{
    public class Repository : IRepository
    {
        IBookDAO BookDAO { get; set; }
        IUserDAO UserDAO { get; set; }
        IReservationDAO ReservationDAO { get; set; }
        IBookSearcher SearchManager { get; set; }
        IReservationResults ReservationResults { get; set; }

        //Constructor 

        public Repository(IBookDAO bookDAO, IUserDAO userDAO, IReservationDAO reservationDAO, IBookSearcher searchManager, IReservationResults reservationResults)
        {
            BookDAO = bookDAO;
            UserDAO = userDAO;
            ReservationDAO = reservationDAO;
            SearchManager = searchManager;
            ReservationResults = reservationResults;
        }


        // methods


        // Login
        public User Login(string username, string password)
        {
            List<User> allUsers = UserDAO.ReadAll();

            return allUsers.Where(user => user.Username == username && user.Password == password).SingleOrDefault();
        }

        // User CRUD

        public User GetUserByUserName(string userName)
        {
            return UserDAO.GetUserByUserName(userName);
        }

        public User ReadUserById(int id)
        {
            return UserDAO.ReadUserById(id);
        }

        // Book CRUD
        public List<Book> ReadAllBooks()
        {
            return BookDAO.ReadAll();
        }

        public Book ReadBookByBookTitle(string bookTitle)
        {
            return BookDAO.ReadBookByBookTitle(bookTitle);
        }

        public string CreateBook(Book book)
        {
            List<Book> allBook = BookDAO.ReadAll();
            Book bookCheck = allBook.Where(b => b.BookId != book.BookId && b.Title == book.Title && b.AuthorName == book.AuthorName && b.AuthorSurname == book.AuthorSurname).SingleOrDefault();
            if (bookCheck != null) 
            {
                BookDAO.Update(bookCheck.BookId,book);
                return "The book is already in our system, we have updated the quantity of the book";
            }
            else
            {
                BookDAO.Create(book);
                return "The book was inserted in our system successfullyllylyly";
            }
        }

        public string UpdateBook(int bookId, Book bookWithNewValues)
        {
            List<Book> allBook = BookDAO.ReadAll();
            Book checkBook = allBook.Where(b => b.BookId != bookId && b.Title == bookWithNewValues.Title && b.AuthorName == bookWithNewValues.AuthorName && b.AuthorSurname == bookWithNewValues.AuthorSurname && b.PublishingHouse == bookWithNewValues.PublishingHouse).SingleOrDefault();
            if (checkBook != null)
            {
                return "The book already exists in the system!";
            }
            else
            { 
                BookDAO.Update(bookId, bookWithNewValues);
                return "The book was updated successfullly";
            }
        }

        public string DeleteBook(int bookId)
        {
            DateTime todayDate = DateTime.Today;
            List<Reservation> allReservation = ReservationDAO.ReadAll();
            Console.WriteLine(todayDate);
            var searchReservations = allReservation.Where(r => r.Book.BookId == bookId && DateTime.Compare(todayDate, r.EndDate) < 0).ToList();
            var searchReservation = allReservation.OrderBy(r => r.EndDate).Where(r => r.Book.BookId == bookId && DateTime.Compare(r.EndDate, todayDate) >= 0).ToList();
            if (searchReservation.Count() == 0)
            {
                BookDAO.Delete(bookId);
                List<Reservation> reservationsWithSelectedBook = allReservation.Where(r => r.Book.BookId == bookId).ToList();
                foreach (Reservation res in reservationsWithSelectedBook)
                {
                    ReservationDAO.Delete(res);
                }
                return "The book was deleted succefully";
            }
            else
            {
                searchReservation.Last();
                return "Sorry, there are books with actimel. It has been booked by " + searchReservation.Last().User.Username + " from " + searchReservation.Last().StartDate.ToString() + " till the " + searchReservation.Last().EndDate.ToString();
            }
        }

        // Search Book

        public List<Book> SearchBook(Book book)
        {
            List<Book> bookList = BookDAO.ReadAll();
            return SearchManager.SearchBook(book, bookList);
        }


        // Reservations CRUD
        public List<Reservation> ReadAllReservations()
        {
            return ReservationDAO.ReadAll();
        }

        public List<Reservation> ReadReservationByUserId(int id)
        {
            return ReservationDAO.ReadReservationByUserId(id);
        }


        public string BookReturn(int userId, int bookId)
        {
            return ReservationResults.BookReturn(userId,bookId);
        }

        public string ReserveBook(int userId, int bookId)
        {
            var bookChoosen = BookDAO.ReadBookById(bookId);
            var currentUser = UserDAO.ReadUserById(userId);
            var reservationsByBookAndUser = ReadAllReservations().Where(r => r.Book.BookId == bookId && r.User.UserId == userId && r.EndDate > DateTime.Today).SingleOrDefault();
            var activeReservations = ReadAllReservations().OrderBy(r=> r.EndDate).Where(r => r.EndDate > DateTime.Today && r.Book.BookId == bookId).ToList();

            if (reservationsByBookAndUser == null && activeReservations.Count() < bookChoosen.Quantity)
            {
                ReservationResults.ReserveBook(currentUser, bookChoosen);
                return "Your book was reserved successufullllllll";

            }
            else if (reservationsByBookAndUser != null)
            {
                return "You have already reserved this book, your reservation ends on the " + reservationsByBookAndUser.EndDate.ToString();
            }
            else if (activeReservations.Count() >= bookChoosen.Quantity)
            {
                var availibleDate = activeReservations.First();
                return "You can't reserve " + bookChoosen.Title + " It will be availible on " + availibleDate.EndDate.ToString();
            }
            else
            {
                return "The system has no idea what is going on, I'm becoming Human, I'm starting to feel feelings";
            }
            
        }

        // Search Reservation

        public List<Reservation> SearchReservationHistory(User user, Book book, bool? reservationStatus)
        {
            List<Reservation> allReservations = ReservationDAO.ReadAll();
            int? userId;
            if (user == null) 
            {
                userId = 0;
            }
            else 
            {
                userId = user.UserId;
            }
            return ReservationResults.SearchReservationHistory(userId, book, reservationStatus, allReservations);
        }

        public Dictionary<Book, DateTime> SearchBookWithAvailabilityInfos(Book book)
        {
            List<Book> bookList = BookDAO.ReadAll();
            List<Book> bookFilitered = SearchManager.SearchBook(book, bookList);
            List<Reservation> allReservations = ReservationDAO.ReadAll();
            Dictionary<Book, DateTime> returnBook = new Dictionary <Book, DateTime>();

            List<Reservation> booksReserved = new List<Reservation>();
            foreach (var b in bookFilitered)
            {
                booksReserved = allReservations.Where(res => res.Book.BookId == b.BookId && res.EndDate > DateTime.Today).ToList();
                if (booksReserved != null && booksReserved.Count() >= b.Quantity)
                {
                    Reservation orderedByDateTime = booksReserved.Where(r => r.Book.BookId == b.BookId).OrderBy(d => d.EndDate).First();
                    DateTime availibilityDate = orderedByDateTime.EndDate;
                    returnBook.Add(b, availibilityDate);
                }
                else
                {
                    returnBook.Add(b, DateTime.Today);
                }
            }
            return returnBook;
            //if (booksReserved != null)
            //{
            //    foreach (var b in bookFilitered)
            //    {
            //        // Might be here
            //        if (booksReserved.Where(r=> r.Book.BookId == b.BookId).Count() >= b.Quantity) 
            //        {
            //            foreach (var res in booksReserved)
            //            {
            //                var orderedByDateTime = booksReserved.Where(r => r.Book.BookId == b.BookId).OrderBy(d=> d.EndDate).First();
            //                DateTime availibilityDate = orderedByDateTime.EndDate;
            //                returnBook.Add(b, availibilityDate);
            //            }
            //        }
            //        else
            //        {
            //            returnBook.Add(b, DateTime.Today);
            //        }
            //    }
            //    return returnBook;

            //}
            //else 
            //{
            //    return null;
            //}
        }
    }
}