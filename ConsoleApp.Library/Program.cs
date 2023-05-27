using BusinessLogic.Library;
using BusinessLogic.Library.Converters;
using DataAccessLayer.Library;
using DataAccessLayer.Library.Repository;
using DataAccessLayer.Library.ReservationResults;
using DataAccessLayer.Library.ReservationResults.SearchReservations;
using DataAccessLayer.Library.ReservationResults.SearchReservations.Classes_ReservationSearch;
using DataAccessLayer.Library.ReservationResults.SearchReservations.InterfaceReservationSearch;
using DataAccessLayer.Library.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp.Library
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\felicia.pompei\\Documents\\Corso di AEHit\\OOP e .Net\\Solution.Library\\DataAccessLayer.Library\\DataBase.xml";

            // Creating DAOs
            IBookDAO bookDAO = new XMLBookDAO(path);
            IUserDAO userDAO = new XMLUserDAO(path);
            IReservationDAO reservationDAO = new XMLReservationDAO(bookDAO, userDAO, path);

            // Search Reservations
            IReservationSearch reservationUserIdSearch = new UserSearcher();
            IReservationBookTitleSearch reservationBookIdSearch = new BookSearcher();
            IReservationStatusSearch reservationStatusSearch = new ReservationStatusSearcher();

            // Reservation Mamager Searcher
            IReservationSearchManager reservationSearcherManager = new ReservationSearchManager(reservationUserIdSearch, reservationBookIdSearch, reservationStatusSearch);
            IReservationResults reservationResults = new ReservationResults(reservationDAO, bookDAO, reservationSearcherManager);

            // search parameters
            IBookSearcher titleBookSearch = new TitleBookSearcher();
            IBookSearcher authorNameBookSearch = new AuthorNameBookSearcher();
            IBookSearcher authorSurnameBookSearch = new AuthorSurnameBookSearcher();
            IBookSearcher publishingHouseBookSearch = new PublishingHouseBookSearcher();

            // SearchManager
            IBookSearcher searchManager = new SearchManager(titleBookSearch, authorNameBookSearch, authorSurnameBookSearch, publishingHouseBookSearch);

            // Creating Repository
            IRepository repository = new Repository(bookDAO, userDAO, reservationDAO, searchManager, reservationResults);

            // Creating Buisness Logic
            ILibraryBusinessLogic bl = new LibraryBL(repository);

            // Menu Functions

            MenuFunctions.OpeningMessage();
            LoginMenu.LogicLogin(bl);
            MenuFunctions.RecycleMenu(bl);
        }
    }
}
