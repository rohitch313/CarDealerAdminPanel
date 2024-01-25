using Admin.Services.CarService.DataLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.CarService.BuisnessLayer
{
    public class CarServices:ICarServices
    {

        private readonly DealerApisemiFinalContext finalContext;
        private readonly IMapper _mapper;
        public CarServices(DealerApisemiFinalContext _finalContext, IMapper mapper)
        {
            finalContext = _finalContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CarDto>> GetCarDetailsAsync()
        {
            try
            {
                var statedetails = await finalContext.Cars.ToListAsync();

                if (statedetails == null || statedetails.Count == 0)
                {
                    // Return an empty collection if no data is found
                    return new List<CarDto>();
                }

                // Use AutoMapper to map the entities to DTO
                var statesDto = _mapper.Map<IEnumerable<CarDto>>(statedetails);

                return statesDto;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateDetailsAsync", ex);
            }
        }

        public async Task<CarDto> AddCarAsync(CarDto newStateDto)
        {
            try
            {
                // Check if the ModelState is valid
                if (newStateDto == null)
                {
                    throw new ArgumentException("Invalid ModelState");
                }

                // Use AutoMapper to map the DTO to the entity
                var newState = _mapper.Map<Car>(newStateDto);

                // Add the new state to the database
                finalContext.Cars.Add(newState);
                await finalContext.SaveChangesAsync();

                // Map the added entity back to DTO
                var addedStateDto = _mapper.Map<CarDto>(newState);

                return addedStateDto;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in AddStateAsync", ex);
            }
        }

        public async Task<IActionResult> DeleteCarAsync(int id)
        {
            try
            {
                // Find the state in the database by id
                var stateToDelete = await finalContext.Cars.FindAsync(id);

                // Return 404 Not Found if the state is not found
                if (stateToDelete == null)
                {
                    return new NotFoundResult();
                }

                // Remove the state from the database
                finalContext.Cars.Remove(stateToDelete);
                await finalContext.SaveChangesAsync();

                // Return 204 No Content indicating successful deletion
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in DeleteStateAsync", ex);
            }
        }

        public async Task<CarDto> GetCarByIdAsync(int id)
        {
            try
            {
                var getState = await finalContext.Cars.FindAsync(id);

                if (getState == null)
                {
                    // Return null if no data is found
                    return null;
                }

                // Use AutoMapper to map the entity to DTO
                var showState = _mapper.Map<CarDto>(getState);

                return showState;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateByIdAsync", ex);
            }
        }

        public async Task<CarDto> UpdateCarAsync(int id, CarDto updatedCarDto)
        {
            try
            {
                // Check if the ModelState is valid
                if (updatedCarDto == null)
                {
                    throw new ArgumentException("Invalid ModelState");
                }

                // Find the state in the database by id
                var existingState = await finalContext.Cars.FindAsync(id);

                // Return null if the state is not found
                if (existingState == null)
                {
                    return null;
                }

                // Use AutoMapper to update the existing state with the DTO data
                _mapper.Map(updatedCarDto, existingState);

                // Update the state in the database
                finalContext.Cars.Update(existingState);
                await finalContext.SaveChangesAsync();

                // Map the updated entity back to DTO
                var updatedStateDtoResult = _mapper.Map<CarDto>(existingState);

                return updatedStateDtoResult;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in UpdateStateAsync", ex);
            }
        }

    }
}
