using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Admin.Services.Purchase.Apply.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.Purchase.Apply.BusinessLayer.Services
{
    public class PvaMakeService : IPvaMakeService
    {

        private readonly DealerApifinalContext _db;
        private readonly IMapper _mapper;


        public PvaMakeService(DealerApifinalContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<PvaMakeDto> AddMakeAsync(PvaMakeDto newStateDto)
        {
            try
            {
                // Check if the ModelState is valid
                if (newStateDto == null)
                {
                    throw new ArgumentException("Invalid ModelState");
                }

                // Use AutoMapper to map the DTO to the entity
                var newState = _mapper.Map<PvaMaketbl>(newStateDto);

                // Add the new state to the database
                _db.PvaMaketbls.Add(newState);
                await _db.SaveChangesAsync();

                // Map the added entity back to DTO
                var addedStateDto = _mapper.Map<PvaMakeDto>(newState);

                return addedStateDto;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in AddStateAsync", ex);
            }
        }

        public async Task<IActionResult> DeleteMakeAsync(int id)
        {
            try
            {
                // Find the state in the database by id
                var stateToDelete = await _db.PvaMaketbls.FindAsync(id);

                // Return 404 Not Found if the state is not found
                if (stateToDelete == null)
                {
                    return new NotFoundResult();
                }

                // Remove the state from the database
                _db.PvaMaketbls.Remove(stateToDelete);
                await _db.SaveChangesAsync();

                // Return 204 No Content indicating successful deletion
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in DeleteStateAsync", ex);
            }
        }

        public async Task<PvaMakeDto> GetMakeByIdAsync(int id)
        {
            try
            {
                var getState = await _db.PvaMaketbls.FindAsync(id);

                if (getState == null)
                {
                    // Return null if no data is found
                    return null;
                }

                // Use AutoMapper to map the entity to DTO
                var showState = _mapper.Map<PvaMakeDto>(getState);

                return showState;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateByIdAsync", ex);
            }
        }

        public async Task<IEnumerable<PvaMakeDto>> GetMakeDetailsAsync()
        {
            try
            {
                var statedetails = await _db.PvaMaketbls.ToListAsync();

                if (statedetails == null || statedetails.Count == 0)
                {
                    // Return an empty collection if no data is found
                    return new List<PvaMakeDto>();
                }

                // Use AutoMapper to map the entities to DTO
                var statesDto = _mapper.Map<IEnumerable<PvaMakeDto>>(statedetails);

                return statesDto;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateDetailsAsync", ex);
            }
        }

        public async Task<PvaMakeDto> UpdateMakeAsync(int id, PvaMakeDto updatedStateDto)
        {
            try
            {
                // Check if the ModelState is valid
                if (updatedStateDto == null)
                {
                    throw new ArgumentException("Invalid ModelState");
                }

                // Find the state in the database by id
                var existingState = await _db.PvaMaketbls.FindAsync(id);

                // Return null if the state is not found
                if (existingState == null)
                {
                    return null;
                }

                // Use AutoMapper to update the existing state with the DTO data
                _mapper.Map(updatedStateDto, existingState);

                // Update the state in the database
                _db.PvaMaketbls.Update(existingState);
                await _db.SaveChangesAsync();

                // Map the updated entity back to DTO
                var updatedStateDtoResult = _mapper.Map<PvaMakeDto>(existingState);

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
