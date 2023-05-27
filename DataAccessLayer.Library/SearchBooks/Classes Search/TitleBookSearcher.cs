using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Search
{
    public class TitleBookSearcher : IBookSearcher
    {
        public List<Book> SearchBook(Book book, List<Book> bookList)
        {
            if (!string.IsNullOrEmpty(book.Title))
            {
                return bookList.Where(r => r.Title.ToLower().Contains(book.Title.ToLower())).Distinct().
                ToList();
            }
            else
            {
                return bookList;
            }
        }
    }
}
