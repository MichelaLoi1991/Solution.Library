
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Model.Library
{
    public class User
    {
        
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }


        // constructors

        // default 
        public User() { }

        public User(int userId, string username, string password, Roles role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Role = role;
        }

    }
}
