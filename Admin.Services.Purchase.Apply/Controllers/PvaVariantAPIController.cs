using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PvaVariantAPIController : ControllerBase
    {
        private readonly IPvaVariantService _variantService;

        public PvaVariantAPIController(IPvaVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpGet("GetAllVariant")]
        public async Task<ActionResult<IEnumerable<PvaVariantDto>>> GetVariantDetails()
        {
            try
            {
                var result = await _variantService.GetVariantDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddVariant")]
        public async Task<ActionResult<PvaVariantDto>> AddVariant([FromBody] PvaVariantDto newStateDto)
        {
            try
            {
                var result = await _variantService.AddVariantAsync(newStateDto);
                return CreatedAtAction(nameof(GetVariantDetails), new { id = result.VariantId }, result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteVariant(int id)
        {
            try
            {
                var deletedState = await _variantService.DeleteVariantAsync(id);

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
        public async Task<ActionResult<PvaVariantDto>> UpdateVariant(int id, [FromBody] PvaVariantDto updatedStateDto)
        {
            try
            {
                var result = await _variantService.UpdateVariantAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetVariantById{id}")]
        public async Task<ActionResult<PvaMakeDto>> GetVariantiId(int id)
        {
            try
            {
                var result = await _variantService.GetVariantByIdAsync(id);
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