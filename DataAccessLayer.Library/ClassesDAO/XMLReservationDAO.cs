using DocumentFormat.OpenXml.Office2010.Excel;
using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccessLayer.Library
{
    public class XMLReservationDAO : IReservationDAO
    {
        public IBookDAO BookDAO { get; set; }
        public IUserDAO UserDAO { get; set; }
        public string Path { get; set; }

        public XMLReservationDAO(IBookDAO bookDAO, IUserDAO userDAO, string path)
        {
            BookDAO = bookDAO;
            UserDAO = userDAO;
            Path = path;
        }

        // CRUDS
        
        public List<Reservation> ReadAll()
        {
            XDocument root = XDocument.Load(Path);
            IEnumerable<Reservation> query;
            query = from r in root.Descendants("Reservation")
                    select new Reservation
                    {
                        Id = (int)r.Attribute("ReservationId"),
                        Book = BookDAO.ReadBookById((int)r.Attribute("BookId")),
                        User = UserDAO.ReadUserById((int)r.Attribute("UserId")),
                        StartDate = DateTime.Parse((string)r.Attribute("StartDate")),
                        EndDate = DateTime.Parse((string)r.Attribute("EndDate"))
                    };
            return query.ToList();
        }

        public List<Reservation> ReadReservationByUserId(int userId)
        {
            List<Reservation> allReservations = ReadAll();
            return allReservations.Where(res => res.User.UserId == userId).ToList();
        }

        public void Create(Reservation reservation)
        {
            
            XmlDocument dataBase = new XmlDocument();
            dataBase.Load(Path);
            XElement doc = XElement.Load(Path);
            var idList = from r in doc.Descendants("Reservation")
                         select (int)r.Attribute("ReservationId");
            int lastId = 0;
            
                lastId = idList.Max() + 1;
            XmlNode node = dataBase.SelectSingleNode("//Reservation");
            XmlElement root = dataBase.CreateElement("Reservation");
            root.SetAttribute("ReservationId", lastId.ToString());
            root.SetAttribute("UserId", reservation.User.UserId.ToString());
            root.SetAttribute("BookId", reservation.Book.BookId.ToString());
            root.SetAttribute("StartDate", reservation.StartDate.ToString());
            root.SetAttribute("EndDate", reservation.EndDate.ToString());
            node.AppendChild(root);
            dataBase.Save(Path);
            
        }

        public void Update(Reservation reservation)
        {
            XDocument database = XDocument.Load(Path);
            var query = from res in database.Descendants("Reservation")
                        where (int)res.Attribute("ReservationId") == reservation.Id
                        select res;
            
                query.First().SetAttributeValue("UserId", reservation.User.UserId);
                query.First().SetAttributeValue("BookId", reservation.Book.BookId);
                query.First().SetAttributeValue("StartDate", reservation.StartDate);
                query.First().SetAttributeValue("EndDate", reservation.EndDate);
                database.Save(Path);
            
        }

        public void Delete(Reservation reservation)
        {
            XDocument database = XDocument.Load(Path);
            var query = from res in database.Descendants("Reservation")
                        where (int)res.Attribute("ReservationId") == reservation.Id
                        select res;
                query.Remove();
                database.Save(Path);

        }
    }
}
