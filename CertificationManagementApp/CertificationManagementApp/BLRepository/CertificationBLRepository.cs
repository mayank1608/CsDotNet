using CertificationManagementApp.DALRepository;
using CertificationManagementApp.Models;

namespace CertificationManagementApp.BLRepository
{
    public class CertificationBLRepository : ICertificationBLRepository
    {
        private readonly ICertificationDALRepository certiDALRepository;

        public CertificationBLRepository(ICertificationDALRepository certificationDALRepository)
        {
            certiDALRepository = certificationDALRepository;            
        }

        public async Task<List<TblCertification>> GetCertificationDetails()
        {
            var certifications = new List<TblCertification>();
            try
            {
                certifications = await certiDALRepository.GetCertificationDetails();
                return certifications;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return certifications;
            }
        }

        public async Task<List<Mapper>> GetEmployeeCertificationDetails()
        {
            try
            {
                return await certiDALRepository.GetEmployeeCertificationDetails();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<List<Mapper>> GetEmployeeCertificationByProvider(string providerName)
        {
            try
            {
                return await certiDALRepository.GetEmployeeCertificationByProvider(providerName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
