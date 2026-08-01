using CarRentalDTO;
namespace CarRentalBL;

public abstract class Cars
{
    private static int carIDCounter = 100;
    public int CarID { get; private set; }


    protected Cars()
    {
        CarID = ++carIDCounter;
    }

    public abstract CarBookingDTO GetBookingDetails(double distance);

}
