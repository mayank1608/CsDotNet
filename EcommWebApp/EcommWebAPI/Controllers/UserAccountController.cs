using EcommWebAPI.EcomBL;
using EcommWebAPI.Helpers;
using EcommWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _service;

        public UserAccountController(IUserAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccount>>> GetAllUserAccounts()
        {
            try
            {
                var userAccounts = await _service.GetAllUserAccounts();
                return Ok(userAccounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccount>> GetUserAccountById(int id)
        {
            try
            {
                var userAccount = await _service.GetUserAccountById(id);
                if (userAccount == null)
                    return NotFound($"UserAccount with ID {id} not found.");

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAccount(UserAccount userAccount)
        {
            try
            {
                userAccount.Password = PasswordHashHandler.HashPassword(userAccount.Password);
                var createdId = await _service.CreateUserAccount(userAccount);
                return Ok($"UserAccount with ID {createdId} created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserAccount(int id, UserAccount userAccount)
        {
            try
            {
                if (id != userAccount.UserAccountId)
                    return BadRequest("UserAccount ID mismatch.");

                var isUpdated = await _service.UpdateUserAccount(userAccount);
                if (isUpdated)
                    return Ok($"UserAccount with ID {id} updated successfully.");

                return NotFound($"UserAccount with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAccount(int id)
        {
            try
            {
                var isDeleted = await _service.DeleteUserAccount(id);
                if (isDeleted)
                    return Ok($"UserAccount with ID {id} deleted successfully.");

                return NotFound($"UserAccount with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
