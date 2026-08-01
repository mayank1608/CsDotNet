using Microsoft.EntityFrameworkCore;
using TaxiDTOs.Context;
using TaxiDTOs.Models;

namespace TaxiDAL
{
    public class TaxiRepository
    {
        private AcmetaxiServiceContext _context;

        public TaxiRepository()
        {
            _context = new AcmetaxiServiceContext();
        }

        public async Task AddCabAsync(Cab cab)
        {
            try
            {
                _context.Cabs.Add(cab);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task AddDriverAsync(Driver driver)
        {
            try
            {
                _context.Drivers.Add(driver);
                await _context.SaveChangesAsync();
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
                cab = await (from c in _context.Cabs
                             where c.CabId == cabId
                             select c).FirstOrDefaultAsync();

                if (cab == null)
                {
                    throw new Exception("Cab not found!");
                }
                return cab;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return cab;
            }
        }

        public async Task<Driver?> GetDriverAsync(string driverId)
        {
            Driver? driver = new Driver();
            try
            {
                driver = await (from d in _context.Drivers
                                where d.DriverId==driverId
                                select d).FirstOrDefaultAsync();
                if (driver == null)
                {
                    throw new Exception("Driver not found!");
                }
                return driver;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return driver;
            }
        }

        public async Task<bool> AllocateTaxiAsync(int cabId, string driverId)
        {
            try{
            
                if (await GetCabAsync(cabId) == null || await GetDriverAsync(driverId) == null)
                    throw new Exception("Invalid Ids");

                AllocateTaxi allocateTaxi = new AllocateTaxi();

                allocateTaxi.CabId = cabId;
                allocateTaxi.DriverId = driverId;

                await _context.AddAsync(allocateTaxi);
                await _context.SaveChangesAsync();

                return true;

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
                var cab = await GetCabAsync(cabId);
                if (cab != null)
                {
                    _context.Remove(cab);
                    await _context.SaveChangesAsync();
                }
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
                var driver = GetDriverAsync(driverId);
                if (driver != null)
                {
                    _context.Remove(driver);
                    await _context.SaveChangesAsync();
                }  
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
                var map = await (from allocation in _context.AllocateTaxis
                                where allocation.MapId == mapId
                                select allocation).FirstOrDefaultAsync();

                if (map != null)
                {
                    _context.Remove(map);
                    await _context.SaveChangesAsync();
                }
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
                AllocateTaxi? record = new AllocateTaxi();
                record = await (from allocation in _context.AllocateTaxis
                                where allocation.MapId == mapId
                                select allocation
                               ).FirstOrDefaultAsync();

                if (record == null)
                    throw new Exception("Map id not found");

                var driver = await GetDriverAsync(driverId);

                if (driver!=null)
                {
                    record.DriverId = driverId;

                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Not a valid driver id.");
                }
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
                AllocateTaxi? record = new AllocateTaxi();

                record = await (from allocation in _context.AllocateTaxis
                                where allocation.MapId == mapId
                                select allocation
                               ).FirstOrDefaultAsync();

                if (record == null)
                    throw new Exception("Map id not found");

                var cab = await GetCabAsync(cabId);
                if (cab != null)
                {
                    record.CabId = cabId;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Not a valid cab id.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public async Task UpdateCabAsync(Cab updateCab)
        {
            try
            {
                _context.Update(updateCab);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task UpdateDriverAsync(Driver updateDriver)
        {
            try
            {
                _context.Update(updateDriver);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
