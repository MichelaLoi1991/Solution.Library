using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Search
{
    public class AuthorSurnameBookSearcher : IBookSearcher
    {
        public List<Book> SearchBook(Book book, List<Book> bookList)
        {
            if(!string.IsNullOrEmpty(book.AuthorSurname))
            {
                return bookList.Where(r => r.AuthorSurname.ToLower().Contains(book.AuthorSurname.ToLower())).Distinct().
                ToList();
            }
            else 
            {
                return bookList;
            }
        }
    }
}
