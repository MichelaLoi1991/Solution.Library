using BusinessLogic.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Library
{
    public class DisplayMenus
    {
        public static void DisplayMenu()
        {

            Console.WriteLine("\t\tMENU");
            Console.WriteLine("0. menu");
            Console.WriteLine("1. Research a book");
            Console.WriteLine("2. Reserve book");
            Console.WriteLine("3. Return book");
            Console.WriteLine("4. See reservation hisotry");
            Console.WriteLine("5. Exit");

        }

        public static void DisplayAdminMenu()
        {
            DisplayMenu();
            Console.WriteLine("\n\t\tAdmin Menu");
            Console.WriteLine("6. Create book");
            Console.WriteLine("7. Modify book");
            Console.WriteLine("8. Delete book");
        }

        public static void DisplayBook(List<BookViewModel> bookList) 
        {
            foreach(var b in bookList)
            {
                Console.WriteLine("Book : {0}", b.Title);
                Console.WriteLine("Author : {0}{1}", b.AuthorName, b.AuthorSurname);
                Console.WriteLine("Publishing House : {0}", b.PublishingHouse);
                Console.WriteLine("");
            }
        }

        public static void DisplayBookWithAvailable(List<BookWithAvailibilityInfoViewModel> bookList)
        {
            foreach (var b in bookList)
            {
                Console.WriteLine("Book : {0}", b.Title);
                Console.WriteLine("Author : {0}{1}", b.AuthorName, b.AuthorSurname);
                Console.WriteLine("Publishing House : {0}", b.PublishingHouse);
                if(b.AvailibleDate > DateTime.Today) 
                {
                    Console.WriteLine($"Book isn't avaible now, it will be availble on {b.AvailibleDate}");
                }
                else 
                {
                    Console.WriteLine("Book is Avaible");
                }
                Console.WriteLine("");
            }
        }
    }
}
