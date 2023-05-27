using BusinessLogic.Library.ViewModels;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library
{
    public interface ILibraryBusinessLogic
    {

        // Login
        LoginResponseViewModel Login(LoginRequestViewModel login);


        // User CRUD
        UserViewModel GetUserByUserName(RequestViewModel userName);

        // Book CRUD
        BookViewModel ReadBookByBookTitle(RequestViewModel bookName);
        List<BookViewModel> ReadAllBooks();
        string Create(BookViewModelWithQuantity book);
        string UpdateBook(RequestViewModel bookName, BookViewModel bookWithNewValues);
        string DeleteBook(RequestViewModel bookName);


        // Search Book
        List<BookViewModel> SearchBook(BookViewModel book);

        // Reservation CRUD
        List<ReservationViewModel> ReadAllReservations();

        List<ReservationViewModel> GetReservationsByUserName(RequestViewModel user);
        
        string ReserveBook(RequestViewModel username, RequestViewModel book);

        string BookReturn(RequestViewModel username, RequestViewModel book);

        // Reservation Search

        List<BookWithAvailibilityInfoViewModel> SearchBookWithAvailabilityInfos(SearchBookViewModel book);

        List<ReservationViewModel> SearchReservationHistory(SearchReservationHistoryViewModel searchReservationHistoryViewModel);
    }
}
