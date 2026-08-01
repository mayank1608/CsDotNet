using CarRentalDTO;
namespace CarRentalBL;

public class PrimeSUV : Cars
{
    public string CarType { get; set; }
    public int NoOfSeats { get; set; }
    public double RatePerKM { get; set; }

    // public PrimeSUV()
    // {
    //     CarType = "Prime SUV";
    //     NoOfSeats = 7;
    //     RatePerKM = 14;
    // }
    public PrimeSUV(string carType, int noOfSeats, double ratePerKm)
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