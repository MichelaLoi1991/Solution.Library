using BusinessLogic.Library;
using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp.Library.Menu
{
    public class MenuChoices
    {
        public static void ChoiceMainMenu()
        {
            if (LoginMenu.currentUser.Role == Roles.Admin)
            {
                DisplayMenus.DisplayAdminMenu();
            }
            else
            {
                DisplayMenus.DisplayMenu();
            }
        }
        public static void ChoiceSearchBook(ILibraryBusinessLogic bl)
        {
            Console.WriteLine("Reserve Book");
            Console.WriteLine("Title of the book you would like to research");
            string searchTitle = Console.ReadLine().ToLower();

            Console.WriteLine("Author Name of the book you would like to research");
            string searchAuthorName = Console.ReadLine().ToLower();

            Console.WriteLine("Author Surname of the book you would like to research");
            string searchAuthorSurname = Console.ReadLine().ToLower();


            Console.WriteLine("Publishing House of the book you would like to research");
            string searchPublishingHouse = Console.ReadLine().ToLower();


            SearchBookViewModel searchBook = new SearchBookViewModel()
            {
                Title = searchTitle,
                AuthorName = searchAuthorName,
                AuthorSurname = searchAuthorSurname,
                PublishingHouse = searchPublishingHouse
            };


            var results = bl.SearchBookWithAvailabilityInfos(searchBook);

            if (results.Count() != 0)
            {
                DisplayMenus.DisplayBookWithAvailable(results);

                //Console.WriteLine("0. Back to Menu");
            }
            else
            {
                Console.WriteLine("There are no results");
                Console.WriteLine("0. Back to Menu");

            }
        }

        public static void ChoiceReserveABook(ILibraryBusinessLogic bl)
        {
            
            Console.WriteLine("Reserve Book");
            Console.WriteLine("Title of the book you would like to research");
            string searchTitle = Console.ReadLine().ToLower();

            Console.WriteLine("Author Name of the book you would like to research");
            string searchAuthorName = Console.ReadLine().ToLower();

            Console.WriteLine("Author Surname of the book you would like to research");
            string searchAuthorSurname = Console.ReadLine().ToLower();


            Console.WriteLine("Publishing House of the book you would like to research");
            string searchPublishingHouse = Console.ReadLine().ToLower();


            SearchBookViewModel searchBook = new SearchBookViewModel()
            {
                Title = searchTitle,
                AuthorName = searchAuthorName,
                AuthorSurname = searchAuthorSurname,
                PublishingHouse = searchPublishingHouse,
                //IsActive = true
            };
            
            
            var results = bl.SearchBookWithAvailabilityInfos(searchBook);

            if (results.Count() != 0)
            {
                DisplayMenus.DisplayBookWithAvailable(results);

                //Console.WriteLine("0. Back to Menu");
            }
            else
            {
                Console.WriteLine("There are no results");
                Console.WriteLine("0. Back to Menu");

            }
            
            Console.WriteLine("Choose what book you would like to reserve");
            string userBookChoice = Console.ReadLine();
            RequestViewModel bookChoice = new RequestViewModel()
            {
                UserRequest = userBookChoice
            };
            RequestViewModel user = new RequestViewModel()
            {
                UserRequest = LoginMenu.currentUser.UserName
            };
            string msg = bl.ReserveBook(user, bookChoice);
            Console.WriteLine(msg);

            Console.WriteLine("0. Back to Menu");
        }

        public static void ChoiceRuturnABook(ILibraryBusinessLogic bl) 
        {
            Console.WriteLine("Return Book");
            RequestViewModel user = new RequestViewModel()
            {
                UserRequest = LoginMenu.currentUser.UserName
            };
            var listOfReservations = bl.GetReservationsByUserName(user);
            foreach (var res in listOfReservations)
            {
                Console.WriteLine("User : {0}", res.User.UserName);
                Console.WriteLine("Book : {0}", res.Book.Title);
                Console.WriteLine("Author : {0}{1}", res.Book.AuthorName, res.Book.AuthorSurname);
                Console.WriteLine("Publishing House : {0}", res.Book.PublishingHouse);
                if (!res.ReservationStatus)
                {
                    Console.WriteLine("Reservation Status : Not Active");
                }
                else
                {
                    Console.WriteLine("Reservation Status : Active");
                }
            }
                Console.WriteLine("Select a Book you would like to return");
            string bookToReturn = Console.ReadLine();
            RequestViewModel bookTitle = new RequestViewModel()
            {
                UserRequest = bookToReturn
            };
            string msg = bl.BookReturn(user, bookTitle);
            Console.WriteLine(msg);
            
            Console.WriteLine("0. Back to Menu");
        }

        public static void ChoiceReservationHistory(ILibraryBusinessLogic bl) 
        {
            Console.WriteLine("See reservation hisotry");
            if (LoginMenu.currentUser.Role == Roles.Admin)
            {
                Console.WriteLine("Title of the book you would like to research");
                string searchTitle = Console.ReadLine().ToLower();
                Console.WriteLine("User you would like to research");
                string searchUser = Console.ReadLine().ToLower();
                Console.WriteLine("Is the book available");
                string searchAvaible = Console.ReadLine().ToLower();
                if (searchAvaible != "true" && searchAvaible != "false") 
                {
                    searchAvaible = null;
                }
                bool? searchAval = bool.Parse(searchAvaible);
                SearchReservationHistoryViewModel searchReservation = new SearchReservationHistoryViewModel()
                {
                    BookTitle = searchTitle,
                    UserName = searchUser,
                    ReservationStatus = searchAval
                };

                var listOfReservation = bl.SearchReservationHistory(searchReservation);
                foreach (var res in listOfReservation)
                {
                    Console.WriteLine("User : {0}", res.User.UserName);
                    Console.WriteLine("Book : {0}", res.Book.Title);
                    Console.WriteLine("Author : {0}{1}", res.Book.AuthorName, res.Book.AuthorSurname);
                    Console.WriteLine("Publishing House : {0}", res.Book.PublishingHouse);
                    if (!res.ReservationStatus)
                    {
                        Console.WriteLine("Reservation Status : Not Active");
                    }
                    else
                    {
                        Console.WriteLine("Reservation Status : Active");
                    }
                }
            }
            else
            {
                RequestViewModel user = new RequestViewModel()
                {
                    UserRequest = LoginMenu.currentUser.UserName
                };
                var listOfReservations = bl.GetReservationsByUserName(user);
                foreach (var res in listOfReservations)
                {
                    Console.WriteLine("User : {0}", res.User.UserName);
                    Console.WriteLine("Book : {0}", res.Book.Title);
                    Console.WriteLine("Author : {0}{1}", res.Book.AuthorName, res.Book.AuthorSurname);
                    Console.WriteLine("Publishing House : {0}", res.Book.PublishingHouse);
                    if (!res.ReservationStatus)
                    {
                        Console.WriteLine("Reservation Status : Not Active");
                    }
                    else
                    {
                        Console.WriteLine("Reservation Status : Active");
                    }
                }
            }
            Console.WriteLine("0. Back to Menu");
        }
        public static void ChoiceCreateBook(ILibraryBusinessLogic bl)
        {
            if (LoginMenu.currentUser.Role == Roles.Admin)
            {
                Console.WriteLine("Create Book");

                Console.WriteLine("Book title");
                string newBookTitle = Console.ReadLine();
                while (string.IsNullOrEmpty(newBookTitle))
                {
                    Console.WriteLine("Your input must to contain at least 3 letters");
                    newBookTitle = Console.ReadLine();
                }

                Console.WriteLine("Book AuthorName");
                string newBookAuthorName = Console.ReadLine();
                while (string.IsNullOrEmpty(newBookAuthorName))
                {
                    Console.WriteLine("Your input must to contain at least 3 letters");
                    newBookAuthorName = Console.ReadLine();
                }

                Console.WriteLine("Book AuthorSurname");
                string newBookAuthorSurname = Console.ReadLine();
                while (string.IsNullOrEmpty(newBookAuthorSurname))
                {
                    Console.WriteLine("Your input must to contain at least 3 letters");
                    newBookAuthorSurname = Console.ReadLine();
                }

                Console.WriteLine("Book PublishingHouse");
                string newBookPublishingHouse = Console.ReadLine();
                while (string.IsNullOrEmpty(newBookPublishingHouse))
                {
                    Console.WriteLine("Your input must to contain at least 3 letters");
                    newBookPublishingHouse = Console.ReadLine();
                }

                Console.WriteLine("Book Quantaty");
                int newBookQuantity;
                Int32.TryParse(Console.ReadLine(), out newBookQuantity);
                while (newBookQuantity == 0)
                {
                    Console.WriteLine("Your input must more than 1");
                    Int32.TryParse(Console.ReadLine(), out newBookQuantity);
                }

                // book to add to the database

                BookViewModelWithQuantity bookToAdd = new BookViewModelWithQuantity()
                {
                    Title = newBookTitle,
                    AuthorName = newBookAuthorName,
                    AuthorSurname = newBookAuthorSurname,
                    PublishingHouse = newBookPublishingHouse,
                    Quantity = newBookQuantity
                };

                // create book
                string msg = bl.Create(bookToAdd);
                Console.WriteLine(msg);

                Console.WriteLine("0. Back to Menu");
            }
            else
            {
                Console.WriteLine("Insert a number in the menu list");
                Console.WriteLine("0. Back to Menu");
            }
        }

        public static void ChoiceModifyBook(ILibraryBusinessLogic bl)
        {
            if (LoginMenu.currentUser.Role == Roles.Admin)
            {
                Console.WriteLine("Modify Book");
                Console.WriteLine("List of book");

                DisplayMenus.DisplayBook(bl.ReadAllBooks());

                Console.WriteLine("Choose the book you would like to modify:");

                string bookTitle = Console.ReadLine();
                RequestViewModel bookTitleToModify = new RequestViewModel()
                {
                    UserRequest = bookTitle
                };
                BookViewModel bookToModify = bl.ReadBookByBookTitle(bookTitleToModify);

                // BookToModify
                Console.WriteLine("Modify title:");
                Console.WriteLine("If title doesn't need modifying press enter");
                string modifiedTitle = Console.ReadLine();
                if (string.IsNullOrEmpty(modifiedTitle))
                {
                    modifiedTitle = bookToModify.Title;
                }
                Console.WriteLine("Modify AuthorName:");
                string modifiedAuthorName = Console.ReadLine();
                if (string.IsNullOrEmpty(modifiedAuthorName))
                {
                    modifiedAuthorName = bookToModify.AuthorName;
                }
                Console.WriteLine("Modify AuthorSurname:");
                string modifiedAuthorSurname = Console.ReadLine();
                if (string.IsNullOrEmpty(modifiedAuthorSurname))
                {
                    modifiedAuthorSurname = bookToModify.AuthorSurname;
                }
                Console.WriteLine("Modify PublishingHouse:");
                string modifiedPublishingHouse = Console.ReadLine();
                if (string.IsNullOrEmpty(modifiedPublishingHouse))
                {
                    modifiedPublishingHouse = bookToModify.PublishingHouse;
                }
                BookViewModel modifiedBook = new BookViewModel()
                {
                    Title = modifiedTitle,
                    AuthorName = modifiedAuthorName,
                    AuthorSurname = modifiedAuthorSurname,
                    PublishingHouse = modifiedPublishingHouse
                };
                string msg = bl.UpdateBook(bookTitleToModify, modifiedBook);
                Console.WriteLine(msg);
                Console.WriteLine("0. Back to Menu");
            }
            else
            {
                Console.WriteLine("Insert a number in the menu list");
            }
        }

        public static void ChoiceDeleteBook(ILibraryBusinessLogic bl) 
        {
            if (LoginMenu.currentUser.Role == Roles.Admin)
            {
                Console.WriteLine("Delete Book");
                Console.WriteLine("List of book");

                DisplayMenus.DisplayBook(bl.ReadAllBooks());

                Console.WriteLine("Choose the book you would like to delete:");

                string bookTitleSelected = Console.ReadLine();
                RequestViewModel bookToDelete = new RequestViewModel()
                {
                    UserRequest = bookTitleSelected
                };

                string msg = bl.DeleteBook(bookToDelete);
                Console.WriteLine(msg); 
                Console.WriteLine("0. Back to Menu");
            }
            else
            {
                Console.WriteLine("Insert a number in the menu list");
                Console.WriteLine("0. Back to Menu");
            }
        }
    }
}
