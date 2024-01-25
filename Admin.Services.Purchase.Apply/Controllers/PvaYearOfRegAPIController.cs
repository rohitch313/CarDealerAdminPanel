using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PvaYearOfRegAPIController : ControllerBase
    {
        private readonly IPvaYearOfRegService _yearService;

        public PvaYearOfRegAPIController(IPvaYearOfRegService yearService)
        {
            _yearService = yearService;
        }

        [HttpGet("GetAllYear")]
        public async Task<ActionResult<IEnumerable<PvaYearOfRegDto>>> GetMakeDetails()
        {
            try
            {
                var result = await _yearService.GetYearDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddMake")]
        public async Task<ActionResult<PvaYearOfRegDto>> AddMake([FromBody] PvaYearOfRegDto newStateDto)
        {
            try
            {
                var result = await _yearService.AddYearAsync(newStateDto);
                return CreatedAtAction(nameof(GetMakeDetails), new { id = result.YearId }, result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteMake(int id)
        {
            try
            {
                var deletedState = await _yearService.DeleteYearAsync(id);

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
        public async Task<ActionResult<PvaYearOfRegDto>> UpdateMake(int id, [FromBody] PvaYearOfRegDto updatedStateDto)
        {
            try
            {
                var result = await _yearService.UpdateYearAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetStateById{id}")]
        public async Task<ActionResult<PvaYearOfRegDto>> GetMakeiId(int id)
        {
            try
            {
                var result = await _yearService.GetYearByIdAsync(id);
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