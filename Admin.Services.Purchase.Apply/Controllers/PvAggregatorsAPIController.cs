using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PvAggregatorsAPIController : ControllerBase
    {
        private readonly IAggregatorsService _aggregatorsService;

        public PvAggregatorsAPIController(IAggregatorsService aggregatorsService)
        {
            _aggregatorsService = aggregatorsService;
        }

        [HttpGet("GetAllAggregators")]
        public async Task<ActionResult<IEnumerable<PvAggregatorsDto>>> GetAggregatorsDetails()
        {
            try
            {
                var result = await _aggregatorsService.GetAggregatorDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAggregator(int id)
        {
            try
            {
                var deletedAggregator = await _aggregatorsService.DeleteAggregatorAsync(id);

                if (deletedAggregator != null)
                {
                    // Optionally, you can return information about the deleted aggregator
                    return Ok(new { Message = "Aggregator deleted successfully", DeletedAggregator = deletedAggregator });
                }
                else
                {
                    // Handle the case where the aggregator was not found
                    return NotFound(new { Message = "Aggregator not found" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetAggregatorById/{id}")]
        public async Task<ActionResult<PvAggregatorsDto>> GetAggregatorById(int id)
        {
            try
            {
                var result = await _aggregatorsService.GetAggregatorByIdAsync(id);
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
