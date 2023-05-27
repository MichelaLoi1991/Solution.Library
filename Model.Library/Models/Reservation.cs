using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Library
{
    public class Reservation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        // constructor 

        // default
        public Reservation() { }

        public Reservation(int id, User user, Book book, DateTime startDate, DateTime endDate)
        {
            Id = id;
            User = user;
            Book = book;
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
