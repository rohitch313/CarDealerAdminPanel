using Admin.Services.UserinfoAPI.BusinessLayer.IService;
using Admin.Services.UserinfoAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Services.UserinfoAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class UserInfoAPIController : ControllerBase
    {
        private readonly IUserService _userService;
        private ResponseDto _response;

        public UserInfoAPIController(IUserService userService)
        {
            _userService = userService;
            _response = new ResponseDto();
        }

        [HttpGet("Getdetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserInfoSupport()
        {
            try
            {
                // Call the GetUserDetailsAsync method from the UserService
                var result = await _userService.GetUserDetailsAsync();

                // Return the result from the service
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }



        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteState(int id)
        {
            try
            {
                var deletedState = await _userService.DeleteUserAsync(id);

                if (deletedState != null)
                {
                    // Optionally, you can return information about the deleted state
                    return Ok(new { Message = "State deleted successfully", DeletedState = deletedState });
                }
                else
                {
                    // Handle the case where the state was not found
                    return NotFound(new { Message = "State not found" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        [HttpPut("UpdateId{id}")]
        public async Task<ActionResult<UsersDto>> UpdateState(int id, [FromBody] UsersDto updatedStateDto)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        

        [HttpGet("GetUserById{id}")]
        public async Task<ActionResult<UsersDto>> GetStateiId(int id)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}



