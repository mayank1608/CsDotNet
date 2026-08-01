namespace CarRentalDTO
{
    internal class PrimeSedan : Cars
    {
        public PrimeSedan(int CarID, string carType, int noOfSeats, double ratePerKm) :
                 base(CarID)
        {
            CarType = carType;
            NoOfSeats = noOfSeats;
            RatePerKm = ratePerKm;
        }

        public String CarType { get; set; }
        public int NoOfSeats { get; set; }
        public double RatePerKm { get; set; }

        public override void DisplayBookingDetails(double distance)
        {

        }
    }
}
