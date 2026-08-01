using CertificationManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CertificationManagementApp.DALRepository
{
    public class CertificationDALRepository : ICertificationDALRepository
    {
        public readonly CertificationTrackerDbContext _context;

        public CertificationDALRepository(CertificationTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<List<TblCertification>> GetCertificationDetails()
        {
            var certifications = new List<TblCertification>();
            try
            {
                certifications = await _context.TblCertifications.ToListAsync();
                return certifications;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return certifications;
            }
        }



        public async Task<List<Mapper>> GetEmployeeCertificationDetails()
        {
            try
            {

                var query = from emp in _context.TblEmployees

                            join ec in _context.TblEmployeeCertifications
                            on emp.Psnumber equals ec.EmployeePsnumber

                            join cert in _context.TblCertifications
                            on ec.CertificationId equals cert.CertificationId

                            join lvl in _context.TblCertificationLevels
                            on cert.CertificationLevel equals lvl.ClevelId


                            select new Mapper
                            {
                                PsNo = emp.Psnumber,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                EmailId = emp.EmailId,
                                BusinessUnit = emp.BusinessUnit,
                                CertificationTitle = cert.Title,
                                CertificationLevel = lvl.Clevel,
                                Price = cert.Price
                            };

                return await query.ToListAsync();
            }
            catch
            {
                return new List<Mapper>();
            }
        }

        public async Task<List<Mapper>> GetEmployeeCertificationByProvider(string providerName)
        {

            try
            {
                var query = from emp in _context.TblEmployees

                            join ec in _context.TblEmployeeCertifications
                            on emp.Psnumber equals ec.EmployeePsnumber

                            join cert in _context.TblCertifications
                            on ec.CertificationId equals cert.CertificationId

                            join lvl in _context.TblCertificationLevels
                            on cert.CertificationLevel equals lvl.ClevelId

                            join prov in _context.TblCertificationProviders
                            on cert.CertificationProvider equals prov.CproviderId

                            where prov.Cprovider == providerName

                            select new Mapper
                            {
                                PsNo = emp.Psnumber,
                                EmployeeName = emp.FirstName + " " + emp.LastName,
                                EmailId = emp.EmailId,
                                BusinessUnit = emp.BusinessUnit,
                                CertificationTitle = cert.Title,
                                CertificationLevel = lvl.Clevel,
                                Price = cert.Price
                            };

                return await query.ToListAsync();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return null ;
            }


        }

    }
}
