using Model.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccessLayer.Library
{
    public class XMLBookDAO : IBookDAO
    {

        public string Path { get; set; }

        public XMLBookDAO(string path)
        {
            Path = path;
        }
       
        public List<Book> ReadAll()
        {
            XDocument root = XDocument.Load(Path);
            IEnumerable<Book> query;
            query = from r in root.Descendants("Book")
                    select new Book
                    {
                        BookId = (int)r.Attribute("BookId"),
                        Title = (string)r.Attribute("Title"),
                        AuthorName = (string)r.Attribute("AuthorName"),
                        AuthorSurname = (string)r.Attribute("AuthorSurname"),
                        PublishingHouse = (string)r.Attribute("Publisher"),
                        Quantity = (int)r.Attribute("Quantity")
                    };
            return query.ToList();
        }

        public Book ReadBookById(int id)
        {
            List<Book> allBooks = ReadAll();
            return allBooks.Where(book => book.BookId == id).SingleOrDefault();
        }

        public Book ReadBookByBookTitle(string bookTitle)
        {
            List<Book> allBooks = ReadAll();
            return allBooks.Where(book => book.Title.Equals(bookTitle)).SingleOrDefault();
        }

        public void Create(Book book)
        {
            XmlDocument dataBase = new XmlDocument();
            XDocument.Load(Path);
            dataBase.Load(Path);
            XElement doc = XElement.Load(Path);
            var idList = from r in doc.Descendants("Book")
                         select (int)r.Attribute("BookId");
            int lastId = 0;
            lastId = idList.Max() + 1;
            XmlNode node = dataBase.SelectSingleNode("//Books");
            XmlElement root = dataBase.CreateElement("Book");
            root.SetAttribute("BookId", lastId.ToString());
            root.SetAttribute("Title", book.Title);
            root.SetAttribute("AuthorName", book.AuthorName);
            root.SetAttribute("AuthorSurname", book.AuthorSurname);
            root.SetAttribute("Publisher", book.PublishingHouse);
            root.SetAttribute("Quantity", book.Quantity.ToString());
            node.AppendChild(root);
            dataBase.Save(Path);
        }

        public void Update(int id, Book book)
        {
            XDocument database = XDocument.Load(Path);
            var query = from r in database.Descendants("Book")
                        where (int)r.Attribute("BookId") == id
                        select r;
                query.First().SetAttributeValue("Title", book.Title);
                query.First().SetAttributeValue("AuthorName", book.AuthorName);
                query.First().SetAttributeValue("AuthorSurname", book.AuthorSurname);
                query.First().SetAttributeValue("Publisher", book.PublishingHouse);
                query.First().SetAttributeValue("Quantity", book.Quantity);
            database.Save(Path);
        }

        public void Delete(int id)
        {

            XDocument database = XDocument.Load(Path);
            
            var query = from b in database.Descendants("Book")
                        where (int)b.Attribute("BookId") == id
                        select b;
                query.Remove();
                database.Save(Path);
        }
    }
}
