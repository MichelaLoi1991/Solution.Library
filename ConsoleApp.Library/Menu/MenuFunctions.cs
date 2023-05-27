using BusinessLogic.Library;
using BusinessLogic.Library.ViewModels;
using BusinessLogic.Library.ViewModels.Enums;
using ConsoleApp.Library.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Library
{
    public class MenuFunctions
    {
        static LoginResponseViewModel currentUser;
        static int userChoice;
        

        //methods

        // Welcome

        public static void OpeningMessage()
        {

            Console.WriteLine("Welcome to the Library");
        }

        // Exit from Program
        public static void Exit()
        {
            Console.WriteLine("See You Soon");
            return;
        }

        // Choice

        public static void YesOrNoLogic(ILibraryBusinessLogic bl)
        {
            string choiceYorN = Console.ReadLine();

            if (choiceYorN.ToLower().Equals("y"))
            {
                LoginMenu.LogicLogin(bl);
            }

            else if (choiceYorN.ToLower().Equals("n"))
            {
                Exit();
            }

            else
            {
                Console.WriteLine("Incorrect Selection");
            }
        }

        // Menu choices

        public static void RecycleMenu(ILibraryBusinessLogic bl) 
        {
            while (userChoice != 5)
            {
                //CheckingNumber("Please insert a number that is present on the menu",userChoice);
                Int32.TryParse(Console.ReadLine(), out userChoice);
                 //Console.Clear();
                MenuChoiceLogic(bl);
            }
        }

        // Checking number value
        public static void CheckingNumber(string comment, int intToCheck)
        {
            while (intToCheck == 0)
            {
                Console.WriteLine(comment);
                Int32.TryParse(Console.ReadLine(), out intToCheck);
            }
        }

        // Switch Options
        public static void MenuChoiceLogic(ILibraryBusinessLogic bl)
        {

            switch (userChoice)
            {
                case 0:
                    MenuChoices.ChoiceMainMenu();
                    break;
                case 1:
                    MenuChoices.ChoiceSearchBook(bl);
                    break;
                case 2:
                    MenuChoices.ChoiceReserveABook(bl);
                    break;
                case 3:
                    MenuChoices.ChoiceRuturnABook(bl);
                    break;
                case 4:
                    MenuChoices.ChoiceReservationHistory(bl);
                    break;
                case 5:
                        Exit();
                    break;
                case 6:
                    MenuChoices.ChoiceCreateBook(bl);
                    break;
                case 7:
                    MenuChoices.ChoiceModifyBook(bl);
                    break;
                case 8:
                    MenuChoices.ChoiceDeleteBook(bl);
                    break;

                default:

                    break;
            }
        }
    }
}
