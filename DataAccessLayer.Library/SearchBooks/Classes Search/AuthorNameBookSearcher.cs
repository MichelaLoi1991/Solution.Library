using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Search 
{
    public class AuthorNameBookSearcher : IBookSearcher
    {
        public List<Book> SearchBook(Book book, List<Book> bookList)
        {
            if(!string.IsNullOrEmpty(book.AuthorName))
            {
                return bookList.Where(r => r.AuthorName.ToLower().Contains(book.AuthorName.ToLower())).Distinct().
                ToList();
            }
            else
            {
                return bookList;
            }
        }
    }
}
