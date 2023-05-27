using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library
{
    public interface IBookDAO
    {
        List<Book> ReadAll();
        Book ReadBookById(int id);

        Book ReadBookByBookTitle(string bookTitle);

        void Create(Book book);

        void Update(int id, Book book);

        void Delete(int id);

    }
}
