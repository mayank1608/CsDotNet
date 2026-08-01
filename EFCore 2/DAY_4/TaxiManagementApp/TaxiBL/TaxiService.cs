using Microsoft.EntityFrameworkCore;
using TaxiDAL;
using TaxiDTOs.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaxiBL
{
    public class TaxiService
    {
        private TaxiRepository _repository;

        public TaxiService()
        {
            _repository = new TaxiRepository();
        }

        public async Task AddCabAsync(int setCabId, string carNumber, string model, int year)
        {
            try
            {
                Cab cab = new Cab();    
                cab.CabId = setCabId;
                cab.Carnumber = carNumber;
                cab.Model = model;  
                cab.ManufactureYear = year;

                await _repository.AddCabAsync(cab); 
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task AddDriverAsync(string setDriverId, string firstName, string lastName, string? dlNo, DateOnly date, int? isActive)
        {
            try
            {
                Driver driver = new Driver();
                driver.DriverId = setDriverId;
                driver.FirstName = firstName;
                driver.LastName = lastName;
                driver.Expirydate = date;
                driver.Active = isActive;
                
                await _repository.AddDriverAsync(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<Cab?> GetCabAsync(int cabId)
        {
            Cab? cab = new Cab();

            try
            {
               cab = await _repository.GetCabAsync(cabId); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return cab;
        }

        public async Task<Driver?> GetDriverAsync(string driverId)
        {
            Driver? driver = new Driver();

            try
            {
                driver = await _repository.GetDriverAsync(driverId);    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return driver;
        }

        public async Task<bool> AllocateTaxiAsync(int cabId, string driverId)
        {
            try
            {
                return await _repository.AllocateTaxiAsync(cabId, driverId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task DeleteCabAsync(int cabId)
        {
            try
            {
                await _repository.DeleteCabAsync(cabId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task DeleteDriverAsync(string driverId)
        {
            try
            {
                await _repository.DeleteDriverAsync(driverId);  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task DeallocateTaxiAsync(int mapId)
        {
            try
            {
                await _repository.DeallocateTaxiAsync(mapId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task UpdateAllocateMappingAsync(int mapId, string driverId)
        {
            try
            {
                await _repository.UpdateAllocateMappingAsync(mapId, driverId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public async Task UpdateAllocateMappingAsync(int mapId, int cabId)
        {
            try
            {
                await _repository.UpdateAllocateMappingAsync(mapId, cabId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public async Task UpdateCabAsync(int updateCabId, string updateCarNumber, string updateModel, int updateYear)
        {
            try
            {
                Cab cab = new Cab();
                cab.CabId = updateCabId;
                cab.Carnumber = updateCarNumber;
                cab.Model = updateModel;    
                cab.ManufactureYear = updateYear;

                await _repository.UpdateCabAsync(cab);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task UpdateDriverAsync(string updateDriverId, string updatedFirstName, string updatedLastName, string? updatedDlNo, DateOnly updateDate, int? updatedIsActive)
        {
            try
            {
                Driver driver = new Driver();

                driver.DriverId = updateDriverId;
                driver.FirstName = updatedFirstName;
                driver.LastName = updatedLastName;
                driver.DrivingLicenceNo = updatedDlNo;
                driver.Expirydate = updateDate;
                driver.Active = updatedIsActive;

                await _repository.UpdateDriverAsync(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
