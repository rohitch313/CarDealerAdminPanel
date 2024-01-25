using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PvOpenMarketsAPIController : ControllerBase
    {
        private readonly IPvOpenMarketsService _marketService;

        public PvOpenMarketsAPIController(IPvOpenMarketsService marketService)
        {
            _marketService = marketService;
        }

        [HttpGet("GetAllAggregators")]
        public async Task<ActionResult<IEnumerable<PvOpenMarketsDto>>> GetAggregatorsDetails()
        {
            try
            {
                var result = await _marketService.GetOpenMarketDetailsAsync();
                return Ok(result);
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
                var deletedState = await _marketService.DeleteOpenMarketAsync(id);

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



        [HttpGet("GetStateById{id}")]
        public async Task<ActionResult<PvOpenMarketsDto>> GetMakeiId(int id)
        {
            try
            {
                var result = await _marketService.GetOpenMarketByIdAsync(id);
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