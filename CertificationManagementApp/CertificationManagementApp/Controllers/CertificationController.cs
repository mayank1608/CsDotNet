using CertificationManagementApp.BLRepository;
using CertificationManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CertificationManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CertificationController : Controller
    {
        public readonly ICertificationBLRepository certiBLRepository;

        public CertificationController(ICertificationBLRepository certificationBLRepository)
        {
            certiBLRepository = certificationBLRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TblCertification>>> GetCertificationDetails()
        {
            var certifications = new List<TblCertification>();

            try
            {
                certifications = await certiBLRepository.GetCertificationDetails();
                if (certifications == null)
                    return NotFound("No Certificate Available");
                return Ok(certifications);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeCertificationDetails()
        {
            try
            {
                var result = await certiBLRepository.GetEmployeeCertificationDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByProvider(string providerName)
        {
            try
            {
                var result = await certiBLRepository.GetEmployeeCertificationByProvider(providerName);
                return Ok(result);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
