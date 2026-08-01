using CarRentalDTO;

namespace CarRentalBusinessLayer
{
    public class BusinessLayer
    {
        public void GetCarDetails(Cars CarType,double distance)
        {
          
                CarType.DisplayBookingDetails(distance);
            
        }
    }
}
