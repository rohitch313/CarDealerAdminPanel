using Admin.UI.Models;
using Admin.UI.Service;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace Admin.UI.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        public async Task<ActionResult> StateIndex()
        {
            try
            {
                var result = await _stateService.GetStateDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<StateDto>());
            }
            catch (HttpRequestException ex)
            {
                // Log the exception details
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw; // rethrow the exception to propagate it up the call stack
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");
            }
        }

        public async Task<ActionResult> StateCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StateCreate(StateDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _stateService.AddStateAsync(model);

                    if (result != null)
                    {
                        
                    TempData["successMessage"] = "State Created successfully.";
                        return RedirectToAction(nameof(StateIndex));
                    }
                    else
                    {
                        TempData["dangerMessage"] = "State Creation Failed.";
                        return RedirectToAction(nameof(StateIndex));
                    }
                }
                // If the model state is invalid or the state creation fails, return the view with the model
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");

                // Return the view with the model and the error message
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");
            }
        }


        public async Task<ActionResult> StateDelete(int stateid)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _stateService.DeleteStateAsync(stateid);

                    if (result != null)
                    {
                        TempData["successMessage"] = "State Deleted Successfully.";
                        return RedirectToAction(nameof(StateIndex));
                    }
                    else
                    {
                        // Handle 404 Not Found
                        TempData["dangerMessage"] = "State Deletion Failed.";
                        return RedirectToAction(nameof(StateIndex));
                    }
                }
                return NotFound(); // If ModelState is not valid
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again.");

                // Return the view or appropriate action after catching the exception
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");
            }
        }

        public async Task<ActionResult> StateToUpdate(int stateid)
        {
            try
            {
                var existingState = await _stateService.GetStateByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("StateUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(StateIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> StateUpdate([FromRoute] int stateid, [FromForm] StateDto updatedStateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stateid = updatedStateDto.StateId;
                    var updatedState = await _stateService.UpdateStateAsync(stateid, updatedStateDto);

                    if (updatedState != null)
                    {
                        TempData["successMessage"] = "State Update Successfully.";
                        return RedirectToAction(nameof(StateIndex));
                    }
                    else
                    {
                        TempData["dangerMessage"] = "State Update Failed.";
                        return RedirectToAction(nameof(StateIndex));
                    }
                }

                return View("StateUpdate", updatedStateDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");
            }


        }
    }
}
