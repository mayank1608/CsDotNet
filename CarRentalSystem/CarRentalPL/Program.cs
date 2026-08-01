using CarRentalBL;
using CarRentalDTO;


namespace CarRentalPL
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Rent a Car in Vadodara");
            Console.WriteLine("---------------------");
            Console.WriteLine();

            Console.WriteLine("Travel Date :");
            string? travelDate = Console.ReadLine();

            Console.WriteLine("Choose the type of the car");
            Console.WriteLine("        1 - Micro - 80Km/8hours");
            Console.WriteLine("        2 - Micro - 120 km/12hours");
            Console.WriteLine("        3 - Prime Sedan - 80 km/8hours");
            Console.WriteLine("        4 - Prime Sedan - 120 km/12hours");
            Console.WriteLine("        5 - Prime SUV - 120 km/12hours");

            Console.Write("Your option? ");
            int choice = Convert.ToInt32(Console.ReadLine());

            Cars? selectedCar = null;
            double distance = 0;

            switch (choice)
            {
                case 1:
                    // selectedCar = new Micro();
                    selectedCar = new Micro("Micro", 4, 9);
                    distance = 80;
                    Console.WriteLine("You have chosen Micro");
                    break;

                case 2:
                    selectedCar = new Micro("Micro", 4, 9);
                    distance = 120;
                    Console.WriteLine("You have chosen Micro");
                    break;

                case 3:
                    // selectedCar = new PrimeSedan();
                    selectedCar = new PrimeSedan("Prime Sedan", 4, 12);
                    distance = 80;
                    Console.WriteLine("You have chosen Prime Sedan");
                    break;

                case 4:
                    selectedCar = new PrimeSedan("Prime Sedan", 4, 12);
                    distance = 120;
                    Console.WriteLine("You have chosen Prime Sedan");
                    break;

                case 5:
                    selectedCar = new PrimeSUV("Prime SUV", 7, 14);
                    distance = 120;
                    Console.WriteLine("You have chosen Prime SUV");
                    break;

                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }

            if (selectedCar != null)
            {
                CarBookingDTO bookingDetails = selectedCar.GetBookingDetails(distance);
                DisplayBookingDetails(bookingDetails);
            }

            Console.ReadLine();
        }


        static void DisplayBookingDetails(CarBookingDTO bookingDetails)
        {
            Console.WriteLine($"Car ID {bookingDetails.CarID}");
            Console.WriteLine($"Car Type : {bookingDetails.CarType}");
            Console.WriteLine($"Number of seats : {bookingDetails.NoOfSeats}");
            Console.WriteLine($"Rate Per km : {bookingDetails.RatePerKM}");
            Console.WriteLine($"Fare : {bookingDetails.Fare}");
        }

    }
}