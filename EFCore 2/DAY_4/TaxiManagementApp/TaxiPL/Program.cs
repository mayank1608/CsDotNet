using TaxiBL;
using TaxiDTOs.Models;

namespace TaxiPL
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                TaxiService taxiService = new TaxiService();
                
                Console.Write("Welcome to Taxi Management, Mysore\n\n" +
                    "1.Display cab by id.\n" +
                    "2.Display driver by id.\n" +
                    "3.Add cab.\n4.Add driver.\n" +
                    "5.Allocate taxi.\n" +
                    "6.Remove cab.\n" +
                    "7.Remove driver\n" +
                    "8.Deallocate Taxi\n" +
                    "9.Update cab\n"+
                    "10.Update driver\n"+
                    "11.Update allocation\n" +
                    "\nEnter your choice: ");
                
                int choice = int.Parse(Console.ReadLine() ?? "0");

                switch (choice)
                {
                    case 1:

                        Console.Write("\nEnter cab id: ");
                        int getCabId = int.Parse(Console.ReadLine() ?? "0");

                        Cab? cab = await taxiService.GetCabAsync(getCabId);
                        
                        if (cab != null)
                        {
                            Console.WriteLine("\nCab Details:-");
                            Console.WriteLine("CabId: "+ cab.CabId);
                            Console.WriteLine("Car Number: "+ cab.Carnumber);
                            Console.WriteLine("Model: "+ cab.Model);
                            Console.WriteLine("Manufactured Year: "+ cab.ManufactureYear);
                        }

                        break;

                    case 2:

                        Console.Write("\nEnter driver id: ");
                        string? getDriverId = Console.ReadLine();

                        if (getDriverId == null)
                            throw new Exception("Id can not be null");

                        Driver? driver = await taxiService.GetDriverAsync(getDriverId);
                        
                        if(driver != null)
                        {
                            Console.WriteLine("\nDriver Details:-");
                            Console.WriteLine("DriverId: " + driver.DriverId);
                            Console.WriteLine("First Name: " + driver.FirstName);
                            Console.WriteLine("Last Name: " + driver.LastName);
                            Console.WriteLine("DL No: " + driver.DrivingLicenceNo);
                            Console.WriteLine("Expiry Date: " + driver.Expirydate);
                            Console.WriteLine("Is Active: " + driver.Active);
                        }

                        break;

                    case 3:

                        Console.Write("\nEnter CabId: ");
                        int setCabId = int.Parse(Console.ReadLine() ?? "0");

                        Console.Write("Enter Car Number:");
                        string carNumber = Console.ReadLine() ?? "";

                        Console.Write("Enter Model: ");
                        string model = Console.ReadLine() ?? "";

                        Console.Write("Enter Manufactured Year: ");
                        int year = int.Parse(Console.ReadLine() ?? "0");

                        await taxiService.AddCabAsync(setCabId, carNumber, model, year);

                        break;

                    case 4:

                        Console.Write("\nEnter DriverId: ");
                        string setDriverId = Console.ReadLine() ?? "0";

                        Console.Write("Enter First Name: ");
                        string firstName = Console.ReadLine() ?? "0";

                        Console.Write("Enter Last Name: ");
                        string lastName = Console.ReadLine() ?? "0";

                        Console.Write("Enter DL No: ");
                        string? dlNo = Console.ReadLine() ?? "0";

                        Console.Write("Enter Expiry Date: ");
                        string inputDate = Console.ReadLine()??"";

                        DateOnly date;
                        if (DateOnly.TryParse(inputDate, out DateOnly expiry))
                        {
                            date = expiry;
                        }
                        else
                        {
                            throw new Exception("Invalid date format. Please use YYYY-MM-DD.");
                        }

                        Console.Write("Enter Is Active: ");
                        string inputIsActive = Console.ReadLine()??"";

                        int? isActive = null;
                        if (int.TryParse(inputIsActive, out int result))
                        {
                            isActive = result;
                        }

                        await taxiService.AddDriverAsync(setDriverId, firstName, lastName, dlNo, date, isActive);

                        break;

                    case 5:

                        Console.Write("\nEnter DriverId: ");
                        string allocateDriverId = Console.ReadLine() ?? "0";

                        Console.Write("Enter CabId: ");
                        int allocateCabId = int.Parse(Console.ReadLine() ?? "0");

                        bool allocation = await taxiService.AllocateTaxiAsync(allocateCabId, allocateDriverId);
                        if (allocation)
                            Console.WriteLine("\nTaxi has been successfully allocated.");
                        else
                            Console.WriteLine("\nSorry, allocation failed!");

                        break;

                    case 6:

                        Console.Write("\nEnter cab id: ");
                        int removeCabId = int.Parse(Console.ReadLine() ?? "0");

                        await taxiService.DeleteCabAsync(removeCabId);

                        break;

                    case 7:

                        Console.Write("\nEnter driver id: ");
                        string? removeDriverId = Console.ReadLine();
                        
                        if (String.IsNullOrWhiteSpace(removeDriverId))
                            throw new Exception("Id can not be null");
                        
                        await taxiService.DeleteDriverAsync(removeDriverId);

                        break;

                    case 8:

                        Console.Write("\nEnter map id: ");
                        int mapId = int.Parse(Console.ReadLine() ?? "0");

                        if (mapId <= 0)
                            throw new Exception("Invalid MapId");

                        await taxiService.DeallocateTaxiAsync(mapId);

                        break;

                    case 9:
                        Console.Write("\nEnter CabId: ");
                        int updateCabId = int.Parse(Console.ReadLine() ?? "0");

                        Console.Write("Enter Car Number:");
                        string updateCarNumber = Console.ReadLine() ?? "";

                        Console.Write("Enter Model: ");
                        string updateModel = Console.ReadLine() ?? "";

                        Console.Write("Enter Manufactured Year: ");
                        int updateYear = int.Parse(Console.ReadLine() ?? "0");
                        
                        await taxiService.UpdateCabAsync(updateCabId, updateCarNumber, updateModel, updateYear);

                        break;

                    case 10:
                        Console.Write("\nEnter DriverId: ");
                        string updateDriverId = Console.ReadLine() ?? "0";

                        Console.Write("Enter First Name: ");
                        string updatedFirstName = Console.ReadLine() ?? "0";

                        Console.Write("Enter Last Name: ");
                        string updatedLastName = Console.ReadLine() ?? "0";

                        Console.Write("Enter DL No: ");
                        string? updatedDlNo = Console.ReadLine() ?? "0";

                        Console.Write("Enter Expiry Date: ");
                        string updatedInputDate = Console.ReadLine() ?? "";

                        DateOnly updateDate;
                        if (DateOnly.TryParse(updatedInputDate, out DateOnly updatedExpiry))
                        {
                            updateDate = updatedExpiry;
                        }
                        else
                        {
                            throw new Exception("Invalid date format. Please use YYYY-MM-DD.");
                        }

                        Console.Write("Enter Is Active: ");
                        string updatedInputIsActive = Console.ReadLine() ?? "";

                        int? updatedIsActive = null;
                        if (int.TryParse(updatedInputIsActive, out int updatedResult))
                        {
                            updatedIsActive = updatedResult;
                        }

                        await taxiService.UpdateDriverAsync(updateDriverId, updatedFirstName, updatedLastName, updatedDlNo, updateDate, updatedIsActive);

                        break;

                    case 11:

                        Console.Write("\n1.Update DriverId\n2.Update CabId\nEnter your choice: ");
                        string? updateChoice = Console.ReadLine();
                        
                        if (updateChoice == "1")
                        {
                            Console.Write("Enter MapID: ");
                            int mapID = int.Parse(Console.ReadLine() ?? "");

                            Console.Write("Enter DriverId: ");
                            string driverID = Console.ReadLine()??"";

                            await taxiService.UpdateAllocateMappingAsync(mapID, driverID);
                        }
                        else if (updateChoice == "2")
                        {
                            Console.Write("Enter MapID: ");
                            int mapID = int.Parse(Console.ReadLine() ?? "");

                            Console.Write("Enter CabId: ");
                            int cabID = int.Parse(Console.ReadLine() ?? "");

                            await taxiService.UpdateAllocateMappingAsync(mapID, cabID);
                        }
                        else
                        {
                            throw new Exception("Not a valid updating choice!");
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
