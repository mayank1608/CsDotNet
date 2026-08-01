using CarRentalBusinessLayer;
using CarRentalDTO;

namespace CarRentalSystemPL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rent a Car in Mysore");
            Console.WriteLine("---------------------------");

            Console.Write("Travel Date: ");
            string travelDate = Console.ReadLine();

            int choice;

            do
            {
                Console.WriteLine("\nChoose the type of the car:");
                Console.WriteLine("1. Micro");
                Console.WriteLine("2. Micro (120 KM / 12 Hr)");
                Console.WriteLine("3. Prime Sedan");
                Console.WriteLine("4. Prime SUV");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());
                BusinessLayer BLObj = new BusinessLayer();

                switch (choice)
                {
                    case 1:
                        Micro MicroObj = new Micro(1,"Micro",4,9);
                        BLObj.GetCarDetails(MicroObj, 70.0);
                        //Display 
                        break;

                    case 2:
                        Console.WriteLine("You selected Micro (120 KM / 12 Hr)");
                        break;

                    case 3:
                        Console.WriteLine("You selected Prime Sedan");
                        break;

                    case 4:
                        Console.WriteLine("You selected Prime SUV");
                        break;

                    case 5:
                        Console.WriteLine("Thank you for using Rent a Car in Mysore!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

            } while (choice != 5);
        }
    }
}
