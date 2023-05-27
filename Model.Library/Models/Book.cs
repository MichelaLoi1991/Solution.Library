using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string PublishingHouse { get; set; }
        public int Quantity { get; set; }


        // constructors
        //default
        public Book()
        {

        }

        public Book(int bookId, string title, string authorName, string authorSurname, string publishingHouse, int quantity)
        {
            BookId = bookId;
            Title = title;
            AuthorName = authorName;
            AuthorSurname = authorSurname;
            PublishingHouse = publishingHouse;
            Quantity = quantity;
        }
        
        // methods

        public string FullAuthorName(string AuthorName, string AuthorSurname)
        {
            return AuthorName + " " + AuthorSurname;
        }
    }
}
