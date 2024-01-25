using Admin.Services.CarService.BuisnessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {

        private readonly ICarServices _carservice;

        public CarController(ICarServices carservice)
        {
            _carservice = carservice;
        }

        [HttpGet("GetAllCar")]
        public async Task<ActionResult<IEnumerable<CarDto>>> GetCarDetails()
        {
            try
            {
                var result = await _carservice.GetCarDetailsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPost("AddCar")]
        public async Task<ActionResult<CarDto>> AddCar([FromBody] CarDto newStateDto)
        {
            try
            {
                var result = await _carservice.AddCarAsync(newStateDto);
                return CreatedAtAction(nameof(GetCarDetails), new { id = result.CarId }, result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            try
            {
                var deletedState = await _carservice.DeleteCarAsync(id);

                if (deletedState != null)
                {
                    // Optionally, you can return information about the deleted state
                    return Ok(new { Message = "State deleted successfully", DeletedState = deletedState });
                }
                else
                {
                    // Handle the case where the state was not found
                    return NotFound(new { Message = "car not found" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


        [HttpPut("Update{id}")]
        public async Task<ActionResult<CarDto>> UpdateCar(int id, [FromBody] CarDto updatedCarDto)
        {
            try
            {
                var result = await _carservice.UpdateCarAsync(id, updatedCarDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("GetCarById{id}")]
        public async Task<ActionResult<CarDto>> GetCariId(int id)
        {
            try
            {
                var result = await _carservice.GetCarByIdAsync(id);
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