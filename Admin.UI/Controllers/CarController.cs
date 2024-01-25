using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.UI.Controllers
{
    public class CarController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ICarService _carService;

        public CarController(IUserInfoService userInfoService, ICarService carService)
        {
            _userInfoService = userInfoService;
            _carService = carService;
        }

        public async Task<ActionResult> CarIndex()
        {
            //TempData["CarSuccessMessage"] = "Admin login successful.";

            try
            {
                
                        


                var result = await _carService.GetCarDetailsAsync();

                if (TempData.ContainsKey("AdminLoginMessage"))
                {
                    ViewBag.AdminLoginMessage = TempData["AdminLoginMessage"];
                }
                if (result != null)
                {
                    // Assuming you have a method to get the states, replace it with your actual logic


                    var states = await _userInfoService.GetUserDetailsAsync();
                    ViewBag.States = states ?? new List<UserInfoDto>(); // Null check
                    ViewBag.StatesCount = result.Count();
                  
                    // Handle the case where result is null, e.g., return an empty view or show an error message
                    
                    return View(result);

                }


                return View(new List<UserInfoDto>());

            }

            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");// rethrow the exception to propagate it up the call stack
            }

        }

        public async Task<ActionResult> CarCreate()
        {
            try
            {

                var users = await _userInfoService.GetUserDetailsAsync(); // Fetch all users from your service

                var userList = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.UserName
                }).ToList();

                ViewBag.UsersDropdown = new SelectList(userList, "Value", "Text"); // Create the SelectList

                return View();
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
        public async Task<ActionResult> CarCreate(CarDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.AddCarAsync(model);

                if (result != null)
                {

                    TempData["CarCreateMessage"] = "Car Create successful.";
                    return RedirectToAction(nameof(CarIndex));

                }
            }
            return View(model);
        }


        public async Task<ActionResult> CarDelete(int carId)
        {
            if (ModelState.IsValid)
            {
                var result = await _carService.DeleteCarAsync(carId);

                if (result != null)
                {
                    TempData["successMessage"] = "Car Delete successful.";
                    return RedirectToAction(nameof(CarIndex));
                }
                else
                {
                    TempData["dangerMessage"] = "failed. Please try again.";
                    return RedirectToAction(nameof(CarIndex));
                }
            }
            return BadRequest(ModelState);
        }

        public async Task<ActionResult> CarToUpdate(int Carid)
        {
            try
            {
                var states = await _userInfoService.GetUserDetailsAsync();
                ViewBag.States = states ?? new List<UserInfoDto>(); // Null check
                ViewBag.StatesCount = ViewBag.States.Count;

                var existingCar = await _carService.GetCarByIdAsync(Carid);

                if (existingCar != null)
                {
                    var associatedUser = states.FirstOrDefault(u => u.Id == existingCar.UserId);

                    // Create a list to hold SelectListItem objects
                    var selectListItems = new List<SelectListItem>();

                    foreach (var user in states)
                    {
                        // Create SelectListItem for each user and set the selected property
                        var item = new SelectListItem
                        {
                            Value = user.Id.ToString(),
                            Text = user.UserName,
                            Selected = (associatedUser != null && user.Id == associatedUser.Id)
                        };
                        selectListItems.Add(item);
                    }

                    // Create SelectList from the list of SelectListItem objects
                    SelectList userList = new SelectList(selectListItems, "Value", "Text");

                    ViewBag.UsersDropdown = userList;

                    return View("UpdateCar", existingCar); // Pass existing car details to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Car with ID {Carid} not found");
                    return RedirectToAction(nameof(CarIndex));
                }
            }
            catch (Exception ex)
            {

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirect to the login page or any other page as needed

                //     ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction("LoginIndex", "Home");
            }
        }


        [HttpPost]
        public async Task<ActionResult> UpdateCar(int id, CarDto updatedCarDto)
        {
            try
            {
                //var states = await _userInfoService.GetUserDetailsAsync();
                //ViewBag.States = states ?? new List<UserInfoDto>(); // Null check
                //ViewBag.StatesCount = ViewBag.States.Count;

                if (ModelState.IsValid)
                {
                    id = updatedCarDto.CarId;
                    var updatedState = await _carService.UpdateCarAsync(id, updatedCarDto);

                    if (updatedState != null)
                    {
                        TempData["UpdateCar"] = "Car Profile Update successful.";
                        return RedirectToAction(nameof(CarIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"User with ID {id} not found");
                        return View("UpdateCar", updatedCarDto);
                    }
                }
                return View("UpdateCar", updatedCarDto);
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

