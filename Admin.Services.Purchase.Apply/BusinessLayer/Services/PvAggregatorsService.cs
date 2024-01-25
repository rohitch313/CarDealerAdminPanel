using Admin.Services.Purchase.Apply.BusinessLayer.DTO;
using Admin.Services.Purchase.Apply.BusinessLayer.IServices;
using Admin.Services.Purchase.Apply.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Services.Purchase.Apply.BusinessLayer.Services
{
    public class PvAggregatorsService : IAggregatorsService
    {
        private readonly DealerApifinalContext _db;
        private readonly IMapper _mapper;


        public PvAggregatorsService(DealerApifinalContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }

        public async Task<IActionResult> DeleteAggregatorAsync(int id)
        {
            try
            {
                var aggregatorToDelete = await _db.PvAggregatorstbls.FindAsync(id);

                if (aggregatorToDelete == null)
                {
                    return new NotFoundResult();
                }

                _db.PvAggregatorstbls.Remove(aggregatorToDelete);
                await _db.SaveChangesAsync();

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteAggregatorAsync", ex);
            }
        }

        public async Task<PvAggregatorsDto> GetAggregatorByIdAsync(int id)
        {
            try
            {
                var aggregatorEntity = await _db.PvAggregatorstbls.FindAsync(id);

                if (aggregatorEntity == null)
                {
                    return null;
                }

                var aggregatorDto = _mapper.Map<PvAggregatorsDto>(aggregatorEntity);

                return aggregatorDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAggregatorByIdAsync", ex);
            }
        }

        public async Task<IEnumerable<PvAggregatorsDto>> GetAggregatorDetailsAsync()
        {
            try
            {
                var aggregatorEntities = await _db.PvAggregatorstbls.ToListAsync();

                if (aggregatorEntities == null || aggregatorEntities.Count == 0)
                {
                    return new List<PvAggregatorsDto>();
                }

                var aggregatorDtos = _mapper.Map<IEnumerable<PvAggregatorsDto>>(aggregatorEntities);

                return aggregatorDtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAggregatorDetailsAsync", ex);
            }
        }
    }
}