using AdminService.BuisnessLayer;
using AdminService.DataLayer;
using AdminService.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminService;

        public AdminController(IAdminServices adminService)
        {
            _adminService = adminService;
        }



        [HttpGet("RegisteredAdmin")]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetCarDetails()
        {
            try
            {
                var result = await _adminService.GetAdminDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminDto adminDTO)
        {
            return await _adminService.RegisterAdmin(adminDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAdmin([FromBody] AdminDto adminDTO)
        {
            return await _adminService.LoginAdmin(adminDTO);
        }
    }
}



