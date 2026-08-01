using CertificationManagementApp.Models;

namespace CertificationManagementApp.BLRepository
{
    public interface ICertificationBLRepository
    {
        public Task<List<TblCertification>> GetCertificationDetails();
        public Task<List<Mapper>> GetEmployeeCertificationDetails();
        public Task<List<Mapper>> GetEmployeeCertificationByProvider(string providerName);
    }
}
