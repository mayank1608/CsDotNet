using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDTO
{
    abstract public class Cars
    {
        public int CarID { get; set; }
         public Cars(int ID)
        {
            CarID = ID;
        }
        abstract public void DisplayBookingDetails(double distance);

    }
}
