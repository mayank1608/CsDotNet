using CarRentalDTO;
namespace CarRentalBL;

public class PrimeSedan : Cars
{
    public string CarType { get; set; }
    public int NoOfSeats { get; set; }
    public double RatePerKM { get; set; }

    // public PrimeSedan()
    // {
    //     CarType = "Prime Sedan";
    //     NoOfSeats = 4;
    //     RatePerKM = 12;
    // }

    public PrimeSedan(string carType, int noOfSeats, double ratePerKm)
    {
        CarType = carType;
        NoOfSeats = noOfSeats;
        RatePerKM = ratePerKm;   
    }

    public override CarBookingDTO GetBookingDetails(double distance)
    {
        double fare = distance * RatePerKM;

        // Console.WriteLine($"Car ID {CarID}");
        // Console.WriteLine($"Car Type : {CarType}");
        // Console.WriteLine($"Number of seats : {NoOfSeats}");
        // Console.WriteLine($"Rate Per km : {RatePerKM}");
        // Console.WriteLine($"Fare : {fare}");

        return new CarBookingDTO
        {
            CarID = CarID,
            CarType = CarType,
            NoOfSeats = NoOfSeats,
            RatePerKM = RatePerKM,
            Distance = distance,
            Fare = fare
        };
    }
}
