using BusinessLogic.Library.ViewModels;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Library.Converters
{
    public class BookMapper
    {
        public static Book BookViewModelToBookConverter(BookViewModel bookViewModel, Book bookToInsert)
        {

            Book book = new Book(bookToInsert.BookId, bookViewModel.Title, bookViewModel.AuthorName, bookViewModel.AuthorSurname, bookViewModel.PublishingHouse, bookToInsert.Quantity);
            return book;
        }

        public static Book BookViewModelWithQuantityToBookConverter(BookViewModelWithQuantity bookViewModelWithQuantity)
        {

            Book book = new Book(0, bookViewModelWithQuantity.Title, bookViewModelWithQuantity.AuthorName, bookViewModelWithQuantity.AuthorSurname, bookViewModelWithQuantity.PublishingHouse, bookViewModelWithQuantity.Quantity);
            return book;
        }

        public static Book BookViewModelSearchToBookConverter(BookViewModel bookViewModel) 
        {
            Book book = new Book(0, bookViewModel.Title, bookViewModel.AuthorName, bookViewModel.AuthorSurname, bookViewModel.PublishingHouse, 0);
            return book;
        }

        public static Book MapSearchBookViewModelToBook(SearchBookViewModel bookViewModel)
        {
            Book book = new Book(0, bookViewModel.Title, bookViewModel.AuthorName, bookViewModel.AuthorSurname, bookViewModel.PublishingHouse, 0);
            return book;
        }

        public static BookViewModel BookToBookViewModelConverter(Book book)
        {
            BookViewModel bookViewModel = new BookViewModel()
            {
                Title = book.Title,
                AuthorName = book.AuthorName,
                AuthorSurname = book.AuthorSurname,
                PublishingHouse = book.PublishingHouse
            };
            return bookViewModel;
            
        }

        public static BookWithAvailibilityInfoViewModel BookToBookWithAvailabilityViewModelConverter(Book book, DateTime dateTime)
        {
            BookWithAvailibilityInfoViewModel bookToAvailableViewModel = new BookWithAvailibilityInfoViewModel()
            {
                Title = book.Title,
                AuthorName = book.AuthorName,
                AuthorSurname = book.AuthorSurname,
                PublishingHouse = book.PublishingHouse,
                AvailibleDate = dateTime
            };
            return bookToAvailableViewModel;

        }
    }
}
