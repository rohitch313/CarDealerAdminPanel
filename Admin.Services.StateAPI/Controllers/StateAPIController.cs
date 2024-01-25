using Admin.Services.StateAPI.Business_layer.IService;
using Admin.Services.StateAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Admin.Services.StateAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class StateAPIController : ControllerBase
    {
        private readonly IStateService _stateService;

        public StateAPIController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet("GetAllState")]
        public async Task<ActionResult<IEnumerable<StatetDto>>> GetStateDetails()
        {
            try
            {
                var result = await _stateService.GetStateDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddState")]
        public async Task<ActionResult<StatetDto>> AddState([FromBody] StatetDto newStateDto)
        {
            try
            {
                var result = await _stateService.AddStateAsync(newStateDto);
                return CreatedAtAction(nameof(GetStateDetails), new { id = result.StateId }, result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteState(int id)
        {
            try
            {
                var deletedState = await _stateService.DeleteStateAsync(id);

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
        public async Task<ActionResult<StatetDto>> UpdateState(int id, [FromBody] StatetDto updatedStateDto)
        {
            try
            {
                var result = await _stateService.UpdateStateAsync(id, updatedStateDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetStateById{id}")]
        public async Task<ActionResult<StatetDto>> GetStateiId(int id)
        {
            try
            {
                var result = await _stateService.GetStateByIdAsync(id);
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
