using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library.Search
{
    public class PublishingHouseBookSearcher : IBookSearcher
    {
        public List<Book> SearchBook(Book book, List<Book> bookList)
        {
            if (!string.IsNullOrEmpty(book.PublishingHouse))
            {
                return bookList.Where(r => r.PublishingHouse.ToLower().Contains(book.PublishingHouse.ToLower())).Distinct().
                ToList();
            }
            else 
            {
                return bookList;
            }
        }
    }
}
