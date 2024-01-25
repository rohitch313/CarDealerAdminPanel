using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PvNewCarDealersAPIController : ControllerBase
    {
        private readonly IPvNewCarDealersService _newcarService;

        public PvNewCarDealersAPIController(IPvNewCarDealersService newcarService)
        {
            _newcarService = newcarService;
        }

        [HttpGet("GetAllAggregators")]
        public async Task<ActionResult<IEnumerable<PvNewCarDealersDto>>> GetAggregatorsDetails()
        {
            try
            {
                var result = await _newcarService.GetNewCarDetailsAsync();
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
                var deletedState = await _newcarService.DeleteNewCarAsync(id);

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
        public async Task<ActionResult<PvNewCarDealersDto>> GetMakeiId(int id)
        {
            try
            {
                var result = await _newcarService.GetNewCarByIdAsync(id);
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