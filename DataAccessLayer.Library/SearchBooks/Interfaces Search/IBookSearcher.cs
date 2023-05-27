using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Search
{
    public interface IBookSearcher
    {
        List<Book> SearchBook(Book book, List<Book> bookList);
    }
}
