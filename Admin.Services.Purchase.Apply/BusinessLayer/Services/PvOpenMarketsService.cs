using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Admin.Services.Purchase.Apply.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.Purchase.Apply.BusinessLayer.Services
{
    public class PvOpenMarketsService : IPvOpenMarketsService
    {
        private readonly DealerApifinalContext _db;
        private readonly IMapper _mapper;


        public PvOpenMarketsService(DealerApifinalContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        public async Task<IActionResult> DeleteOpenMarketAsync(int id)
        {
            try
            {
                // Find the state in the database by id
                var stateToDelete = await _db.PvOpenMarketstbls.FindAsync(id);

                // Return 404 Not Found if the state is not found
                if (stateToDelete == null)
                {
                    return new NotFoundResult();
                }

                // Remove the state from the database
                _db.PvOpenMarketstbls.Remove(stateToDelete);
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

        public async  Task<PvOpenMarketsDto> GetOpenMarketByIdAsync(int id)
        {
            try
            {
                var getState = await _db.PvOpenMarketstbls.FindAsync(id);

                if (getState == null)
                {
                    // Return null if no data is found
                    return null;
                }

                // Use AutoMapper to map the entity to DTO
                var showState = _mapper.Map<PvOpenMarketsDto>(getState);

                return showState;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateByIdAsync", ex);
            }
        }

        public async Task<IEnumerable<PvOpenMarketsDto>> GetOpenMarketDetailsAsync()
        {
            try
            {
                var statedetails = await _db.PvOpenMarketstbls.ToListAsync();

                if (statedetails == null || statedetails.Count == 0)
                {
                    // Return an empty collection if no data is found
                    return new List<PvOpenMarketsDto>();
                }

                // Use AutoMapper to map the entities to DTO
                var statesDto = _mapper.Map<IEnumerable<PvOpenMarketsDto>>(statedetails);

                return statesDto;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateDetailsAsync", ex);
            }
        }
    }
}
