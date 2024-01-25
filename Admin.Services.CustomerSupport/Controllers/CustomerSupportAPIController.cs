using Admin.Services.CustomerSupport.Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.CustomerSupport.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CustomerSupportAPIController : ControllerBase
    {
        private readonly ICustomerSupportService _customerSupport;

        public CustomerSupportAPIController(ICustomerSupportService customerSupport)
        {
            _customerSupport = customerSupport;
        }

        [HttpGet("GetAllState")]
        public async Task<ActionResult<IEnumerable<CustomerSupportDto>>> GetCrustSptDetails()
        {
            try
            {
                var result = await _customerSupport.GetCrustSptDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddState")]
        public async Task<ActionResult<CustomerSupportDto>> AddCrustSpt([FromBody] CustomerSupportDto newStateDto)
        {
            try
            {
                var result = await _customerSupport.AddCrustSptAsync(newStateDto);
                return CreatedAtAction(nameof(GetCrustSptDetails), new { id = result.Email }, result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCrustSpt(int id)
        {
            try
            {
                var deletedState = await _customerSupport.DeleteCrustSptAsync(id);

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


        [HttpPut("Update{id}")]
        public async Task<ActionResult<CustomerSupportDto>> UpdateCrustSpt(int id, [FromBody] CustomerSupportDto updatedStateDto)
        {
            try
            {
                var result = await _customerSupport.UpdateCrustSptAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetStateById{id}")]
        public async Task<ActionResult<CustomerSupportDto>> GetCrustSptId(int id)
        {
            try
            {
                var result = await _customerSupport.GetCrustSptByIdAsync(id);
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
