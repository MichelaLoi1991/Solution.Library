using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Search
{
    public class SearchManager : IBookSearcher
    {
        IBookSearcher TitleBookSearch { get; set; }
        IBookSearcher AuthorNameBookSearch { get; set; }
        IBookSearcher AuthorSurnameBookSearch { get; set; }
        IBookSearcher PublishingHouseBookSearch { get; set; }

        private readonly List<IBookSearcher> filteredBooks = new List<IBookSearcher>();
        
        // constructor
        public SearchManager(IBookSearcher titleBookSearch, IBookSearcher authorNameBookSearch, IBookSearcher authorSurnameBookSearch, IBookSearcher publishingHouseBookSearch) 
        {
            TitleBookSearch = titleBookSearch;
            AuthorNameBookSearch = authorNameBookSearch;
            AuthorSurnameBookSearch = authorSurnameBookSearch;
            PublishingHouseBookSearch = publishingHouseBookSearch;
        }

        //  Methods
        public List<Book> SearchBook(Book book, List<Book> bookList)
        {
           
            filteredBooks.Add(TitleBookSearch);
            filteredBooks.Add(AuthorNameBookSearch);
            filteredBooks.Add(AuthorSurnameBookSearch);
            filteredBooks.Add(PublishingHouseBookSearch);
            
            bool firstList = true;
            List<Book> currentBookList = null;
            List<Book> prevBookList = null;

            foreach (var books in filteredBooks)
            {
                if (!firstList)
                {
                    currentBookList = books.SearchBook(book, prevBookList);
                    prevBookList = currentBookList;
                }
                else 
                {
                    currentBookList = books.SearchBook(book, bookList);
                    prevBookList = currentBookList;
                    firstList = false;
                }
            }
            return currentBookList;
        }
    }
}
