using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Library.ViewModels.Enums;

namespace ConsoleApp.Library
{
    public class LoginMenu
    {
        public static LoginResponseViewModel currentUser;
        public static string InputUsername()
        {
            Console.WriteLine("LOGIN:");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            return username;
        }

        public static string InputPassword()
        {
            Console.Write("Password: ");
            string password = Console.ReadLine();

            return password;

        }

        public static void LogicLogin(ILibraryBusinessLogic bl)
        {

            string username = InputUsername();
            string password = InputPassword();
            LoginRequestViewModel loginRequest = new LoginRequestViewModel()
            {
                UserName = username,
                Password = password
            };
            currentUser = bl.Login(loginRequest);
            if (!currentUser.IsLoggedIn)
            {
                Console.WriteLine("Incorrect Login. Try Again? Type y or n");
                MenuFunctions.YesOrNoLogic(bl);
            }
            else
            {
                if (currentUser.Role == Roles.User)
                {
                    Console.WriteLine($"Welcome {username}");
                    DisplayMenus.DisplayMenu();
                }
                else
                {
                    Console.WriteLine($"Welcome {username}");
                    DisplayMenus.DisplayAdminMenu();
                }

            }
        }
    }
}
