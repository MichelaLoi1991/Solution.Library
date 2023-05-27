using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library
{
    public interface IReservationDAO
    {
        List<Reservation> ReadAll();

        List<Reservation> ReadReservationByUserId(int id);

        void Create(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(Reservation reservation);
    }
}
