namespace CarRentalDTO
{
    public class Micro : Cars
    {
        public Micro(int CarID, string carType, int noOfSeats, double ratePerKm) :
            base(CarID)
        {
            CarType = carType;
            NoOfSeats = noOfSeats;
            RatePerKm = ratePerKm;
        }

        public String CarType {  get; set; }
        public int NoOfSeats { get; set; }
        public double RatePerKm { get; set; }

        public override void DisplayBookingDetails(double distance)
        {
            Console.WriteLine("Card ID : "+ CarID);
            Console.WriteLine("Car Type :"+ CarType);
            Console.WriteLine("Number OF Seats :"+ NoOfSeats);
            Console.WriteLine("Rate Per KM = "+ RatePerKm);
            Console.WriteLine("Fare "+ distance * RatePerKm);
        }
    }
}
