using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Services.Purchase.Apply.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PvaModelAPIController : ControllerBase
    {
        private readonly IPvaModelService _modelService;

        public PvaModelAPIController(IPvaModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet("GetAllModel")]
        public async Task<ActionResult<IEnumerable<PvaModelDto>>> GetModelDetails()
        {
            try
            {
                var result = await _modelService.GetModelDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddModel")]
        public async Task<ActionResult<PvaModelDto>> AddMake([FromBody] PvaModelDto newStateDto)
        {
            try
            {
                var result = await _modelService.AddModelAsync(newStateDto);
                return CreatedAtAction(nameof(GetModelDetails), new { id = result.ModelId }, result);
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
                var deletedState = await _modelService.DeleteModelAsync(id);

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
        public async Task<ActionResult<PvaMakeDto>> UpdateModel(int id, [FromBody] PvaModelDto updatedStateDto)
        {
            try
            {
                var result = await _modelService.UpdateModelAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetStateById{id}")]
        public async Task<ActionResult<PvaModelDto>> GetModeliId(int id)
        {
            try
            {
                var result = await _modelService.GetModelByIdAsync(id);
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