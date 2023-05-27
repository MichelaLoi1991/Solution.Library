
using BusinessLogic.Library.Converters;
using BusinessLogic.Library.ViewModels;
using DataAccessLayer.Library.Repository;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Library
{
    public class LibraryBL : ILibraryBusinessLogic
    {
        IRepository Repository { get; set; }
        
        // constructor
        public LibraryBL(IRepository repository)
        {
            Repository = repository;
        }

        // methods 

        // LogIn

        public LoginResponseViewModel Login(LoginRequestViewModel login)
        {
            LoginResponseViewModel result = new LoginResponseViewModel();

            User currentUser = Repository.Login(login.UserName, login.Password);
            if (currentUser != null)
            {
                result.IsLoggedIn = true;
                result.UserName = currentUser.Username;
                result.Role = currentUser.Role == Roles.Admin ? BusinessLogic.Library.ViewModels.Enums.Roles.Admin : BusinessLogic.Library.ViewModels.Enums.Roles.User;
            }
            else
            {
                result.IsLoggedIn = false;
            }
            return result;
        }

        // User CRUD

        public UserViewModel GetUserByUserName(RequestViewModel userName)
        {
            UserViewModel result = new UserViewModel();
            User currentUser = Repository.GetUserByUserName(userName.UserRequest);
            if (currentUser != null)
            {
                result.UserName = currentUser.Username;
            }
            else
            {
                result = null;
            }
            return result;
        }

        // Book CRUD

        public List<BookViewModel> ReadAllBooks()
        {
            List<Book> bookList = Repository.ReadAllBooks();
            List<BookViewModel> bookListViewModel = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                BookViewModel newBook = BookMapper.BookToBookViewModelConverter(book);
                bookListViewModel.Add(newBook);
            }
            return bookListViewModel;

        }

        public BookViewModel ReadBookByBookTitle(RequestViewModel bookName)
        {
            BookViewModel result = new BookViewModel();
            Book bookSearched = Repository.ReadBookByBookTitle(bookName.UserRequest);
            if (bookSearched != null)
            {
                result = BookMapper.BookToBookViewModelConverter(bookSearched);
            }
            else
            {
                result = null;
            }
            return result;
        }
        public string Create(BookViewModelWithQuantity book)
        {
            Book bookToCreate = BookMapper.BookViewModelWithQuantityToBookConverter(book);
            return Repository.CreateBook(bookToCreate);
        }
        public string UpdateBook(RequestViewModel bookName, BookViewModel bookWithNewValues)
        {
            Book bookToModify = Repository.ReadBookByBookTitle(bookName.UserRequest);
            Book booksNewValues = BookMapper.BookViewModelToBookConverter(bookWithNewValues, bookToModify);
            return Repository.UpdateBook(bookToModify.BookId, booksNewValues);
        }

        public string DeleteBook(RequestViewModel bookName)
        {
            Book bookToDelete = Repository.ReadBookByBookTitle(bookName.UserRequest);
            return Repository.DeleteBook(bookToDelete.BookId);
        }

        // Search Book
        public List<BookViewModel> SearchBook(BookViewModel book)
        {
            Book bookToSearch = BookMapper.BookViewModelSearchToBookConverter(book);
            List<Book> bookList = Repository.SearchBook(bookToSearch);
            List<BookViewModel> bookListViewModel = new List<BookViewModel>();
            foreach (var b in bookList)
            {
                BookViewModel newBook = BookMapper.BookToBookViewModelConverter(b);
                bookListViewModel.Add(newBook);
            }
            return bookListViewModel;
        }

       
        // Reservations
        public List<ReservationViewModel> ReadAllReservations()
        {
            List<Reservation> allReservations = Repository.ReadAllReservations(); 
            List<ReservationViewModel> resersationListViewModel = new List<ReservationViewModel>();
            DateTime today = DateTime.Today;
            foreach (Reservation res in allReservations)
            {
                ReservationViewModel newRes = new ReservationViewModel();
                newRes.User = new UserViewModel
                {
                    UserName = res.User.Username
                };
                newRes.Book = new BookViewModel
                {
                    Title = res.Book.Title,
                    AuthorName = res.Book.AuthorName,
                    AuthorSurname = res.Book.AuthorSurname,
                    PublishingHouse = res.Book.PublishingHouse
                };
                newRes.EndDate = res.EndDate;
                
                if(res.EndDate > today) 
                {
                    newRes.ReservationStatus = true;
                }
                else
                {
                    newRes.ReservationStatus = false;
                }
                resersationListViewModel.Add(newRes);
            }
            return resersationListViewModel;
        }
        public List<ReservationViewModel> GetReservationsByUserName(RequestViewModel user)
        {
            var userSelected = Repository.GetUserByUserName(user.UserRequest);
            List<Reservation> allReservationsByUsername = Repository.ReadReservationByUserId(userSelected.UserId);
            List<ReservationViewModel> resersationListViewModel = new List<ReservationViewModel>();
            DateTime today = DateTime.Today;
            foreach (Reservation res in allReservationsByUsername)
            {
                ReservationViewModel newRes = new ReservationViewModel();
                newRes.User = new UserViewModel
                {
                    UserName = res.User.Username
                };
                newRes.Book = new BookViewModel
                {
                    Title = res.Book.Title,
                    AuthorName = res.Book.AuthorName,
                    AuthorSurname = res.Book.AuthorSurname,
                    PublishingHouse = res.Book.PublishingHouse
                };
                newRes.EndDate = res.EndDate;

                if (res.EndDate > today)
                {
                    newRes.ReservationStatus = true;
                }
                else
                {
                    newRes.ReservationStatus = false;
                }
                resersationListViewModel.Add(newRes);
            }
            return resersationListViewModel;

        }
        public string ReserveBook(RequestViewModel username, RequestViewModel book)
        {
            User userToReserve = Repository.GetUserByUserName(username.UserRequest);
            Book bookToReserve = Repository.ReadBookByBookTitle(book.UserRequest);
            var message = Repository.ReserveBook(userToReserve.UserId, bookToReserve.BookId);
            return message;
        }
        public string BookReturn(RequestViewModel username, RequestViewModel book)
        {
            Book bookToReturn = Repository.ReadBookByBookTitle(book.UserRequest);
            User userToReturn = Repository.GetUserByUserName(username.UserRequest);
            return Repository.BookReturn(userToReturn.UserId, bookToReturn.BookId);
        }


        // Reservation Search
        public List<BookWithAvailibilityInfoViewModel> SearchBookWithAvailabilityInfos(SearchBookViewModel book)
        {
            //Book bookSearched = Repository.ReadBookByBookTitle(book.Title);

            Book bookToSearch = BookMapper.MapSearchBookViewModelToBook(book);
            Dictionary<Book, DateTime> bookListSearch = Repository.SearchBookWithAvailabilityInfos(bookToSearch); 
            List<BookWithAvailibilityInfoViewModel> bookWithAvailibilityInfoViewModel = new List<BookWithAvailibilityInfoViewModel>(); 
            if (bookListSearch != null)
            {
                foreach (var bookFilter in bookListSearch)
                {
                    BookWithAvailibilityInfoViewModel newBook = BookMapper.BookToBookWithAvailabilityViewModelConverter(bookFilter.Key, bookFilter.Value);
                    //newBook.IsActive = true;
                    bookWithAvailibilityInfoViewModel.Add(newBook);
                }
            }
            return bookWithAvailibilityInfoViewModel;
        }
        
        public List<ReservationViewModel> SearchReservationHistory(SearchReservationHistoryViewModel searchReservationHistoryViewModel) 
        {
            Book bookSearched = new Book(0, searchReservationHistoryViewModel.BookTitle, "","","",0);
            User userName = Repository.GetUserByUserName(searchReservationHistoryViewModel.UserName);
            List<Reservation> filteredReservation = Repository.SearchReservationHistory(userName,bookSearched, searchReservationHistoryViewModel.ReservationStatus);
            List<ReservationViewModel> reservationList = new List<ReservationViewModel>();

            if (filteredReservation != null)
            {
                foreach (var res in filteredReservation)
                {
                    ReservationViewModel newRes = new ReservationViewModel();
                    newRes.User = new UserViewModel
                    {
                        UserName = res.User.Username
                    };
                    newRes.Book = new BookViewModel
                    {
                        Title = res.Book.Title,
                        AuthorName = res.Book.AuthorName,
                        AuthorSurname = res.Book.AuthorSurname,
                        PublishingHouse = res.Book.PublishingHouse
                    };
                    newRes.EndDate = res.EndDate;
                    if (res.EndDate > DateTime.Today)
                    {
                        newRes.ReservationStatus = true;
                    }
                    else
                    {
                        newRes.ReservationStatus = false;
                    }
                    reservationList.Add(newRes);
                }
            }
            return reservationList;
        }
    }
}