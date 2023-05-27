using Model.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Library
{
    public interface IUserDAO
    {
        List<User> ReadAll();

        User ReadUserById(int id);

        User GetUserByUserName(string userName);

        void Create(User user);

        void Update(User user);
        void Delete(User user);

        
    }

}
