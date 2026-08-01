namespace CarRentalDTO
{
    public class CarBookingDTO
    {
        public int CarID { get; set; }
        public string CarType { get; set; } = string.Empty;
        public int NoOfSeats { get; set; }
        public double RatePerKM { get; set; }
        public double Distance { get; set; }
        public double Fare { get; set; }
    }
}