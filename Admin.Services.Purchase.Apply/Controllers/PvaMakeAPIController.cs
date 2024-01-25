using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PvaMakeAPIController : ControllerBase
    {
        private readonly IPvaMakeService _makeService;

        public PvaMakeAPIController(IPvaMakeService makeService)
        {
            _makeService = makeService;
        }

        [HttpGet("GetAllMake")]
        public async Task<ActionResult<IEnumerable<PvaMakeDto>>> GetMakeDetails()
        {
            try
            {
                var result = await _makeService.GetMakeDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddMake")]
        public async Task<ActionResult<PvaMakeDto>> AddMake([FromBody] PvaMakeDto newStateDto)
        {
            try
            {
                var result = await _makeService.AddMakeAsync(newStateDto);
                return CreatedAtAction(nameof(GetMakeDetails), new { id = result.MakeId }, result);
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
                var deletedState = await _makeService.DeleteMakeAsync(id);

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
        public async Task<ActionResult<PvaMakeDto>> UpdateMake(int id, [FromBody] PvaMakeDto updatedStateDto)
        {
            try
            {
                var result = await _makeService.UpdateMakeAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetStateById{id}")]
        public async Task<ActionResult<PvaMakeDto>> GetMakeiId(int id)
        {
            try
            {
                var result = await _makeService.GetMakeByIdAsync(id);
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