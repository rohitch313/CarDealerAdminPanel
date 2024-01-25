using Admin.Services.UserinfoAPI.BusinessLayer.IService;
using Admin.Services.UserinfoAPI.Models;
using Admin.Services.UserinfoAPI.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Services.UserinfoAPI.BusinessLayer.Service
{

    public class UserService : IUserService
    {
        private readonly DealerApifinalContext _db;
        private readonly IMapper _mapper;

        public UserService(DealerApifinalContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                // Find the state in the database by id
                var stateToDelete = await _db.Userstbls.FindAsync(id);

                // Return 404 Not Found if the state is not found
                if (stateToDelete == null)
                {
                    return new NotFoundResult();
                }

                // Remove the state from the database
                _db.Userstbls.Remove(stateToDelete);
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

        public async Task<UsersDto> GetUserByIdAsync(int id)
        {
            try
            {
                var getState = await _db.Userstbls.FindAsync(id);

                if (getState == null)
                {
                    // Return null if no data is found
                    return null;
                }

                // Use AutoMapper to map the entity to DTO
                var showState = _mapper.Map<UsersDto>(getState);

                return showState;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                throw new Exception("Error in GetStateByIdAsync", ex);
            }
        }

        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUserDetailsAsync()
        {
            try
            {
                var userInfoDetails = await _db.Userstbls.ToListAsync();

                // Use AutoMapper to map the entities to DTO
                var userInfoDTO = _mapper.Map<IEnumerable<UsersDto>>(userInfoDetails);

                // Return 200 OK with the DTO
                return new OkObjectResult(userInfoDTO);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return new StatusCodeResult(500);
            }
        }

        public async Task<UsersDto> UpdateUserAsync(int id, UsersDto updatedStateDto)
        {
            try
            {
                // Check if the ModelState is valid
                if (updatedStateDto == null)
                {
                    throw new ArgumentException("Invalid ModelState");
                }

                // Find the state in the database by id
                var existingState = await _db.Userstbls.FindAsync(id);

                // Return null if the state is not found
                if (existingState == null)
                {
                    return null;
                }

                // Check if both Active and Rejected flags are true in the updated data
                if (updatedStateDto.Active && updatedStateDto.Rejected)
                {
                    throw new InvalidOperationException("Please select only one option: Active or Rejected.");
                }

                // Use AutoMapper to update the existing state with the DTO data
                _mapper.Map(updatedStateDto, existingState);

                // Update the state in the database only if there are changes

                _db.Userstbls.Update(existingState);
                await _db.SaveChangesAsync();


                // Map the updated entity back to DTO
                var updatedStateDtoResult = _mapper.Map<UsersDto>(existingState);

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
