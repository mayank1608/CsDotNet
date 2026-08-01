using CertificationManagementApp.Models;

namespace CertificationManagementApp.DALRepository
{
    public interface ICertificationDALRepository
    {
        public Task<List<TblCertification>> GetCertificationDetails();
        public Task<List<Mapper>> GetEmployeeCertificationDetails();
        public Task<List<Mapper>> GetEmployeeCertificationByProvider(string providerName);

    }
}
