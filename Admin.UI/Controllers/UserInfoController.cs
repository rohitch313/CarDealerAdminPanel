using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class UserInfoController : Controller
{
    private readonly IUserInfoService _userInfoService;
    private readonly IStateService _stateService;

    public UserInfoController(IUserInfoService userInfoService, IStateService stateService)
    {
        _userInfoService = userInfoService;
        _stateService = stateService;
    }

    public async Task<ActionResult> UserInfoIndex()
    {
        try
        {
            var result = await _userInfoService.GetUserDetailsAsync();

            if (result != null)
            {
                // Assuming you have a method to get the states, replace it with your actual logic


                var states = await _stateService.GetStateDetailsAsync();
                ViewBag.States = states ?? new List<StateDto>(); // Null check

                return View(result);
            }

            // Handle the case where result is null, e.g., return an empty view or show an error message
            return View(new List<UserInfoDto>());
        }
        catch (HttpRequestException ex)
        {
            // Log the exception details
            Console.WriteLine($"HTTP request error: {ex.Message}"); await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the login page or any other page as needed

            //     ModelState.AddModelError(string.Empty, "Internal Server Error");
            return RedirectToAction("LoginIndex", "Home"); throw; // rethrow the exception to propagate it up the call stack
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

    public async Task<ActionResult> UserInfoDelete(int userid)
    {
        if (ModelState.IsValid)
        {
            var result = await _userInfoService.DeleteUserAsync(userid);

            if (result != null)
           {
                    TempData["successMessage"] = "User Delete successful.";
                    return RedirectToAction(nameof(UserInfoIndex));
                }
                else
                {
                    TempData["dangerMessage"] = "failed . Please try again.";
                    return RedirectToAction(nameof(UserInfoIndex));
                }
        }
        return NotFound();
    }

    public async Task<ActionResult> UserToUpdate(int userid)
    {
        try
        {
            var states = await _stateService.GetStateDetailsAsync();
            ViewBag.States = states ?? new List<StateDto>(); // Null check
            ViewBag.StatesCount = ViewBag.States.Count;

            var existingState = await _userInfoService.GetUserByIdAsync(userid);
            if (existingState != null)
            {
                var associatedUser = states.FirstOrDefault(u => u.StateId == existingState.Sid);

                // Create a list to hold SelectListItem objects
                var selectListItems = new List<SelectListItem>();

                foreach (var user in states)
                {
                    // Create SelectListItem for each user and set the selected property
                    var item = new SelectListItem
                    {
                        Value = user.StateId.ToString(),
                        Text = user.StateName,
                        Selected = (associatedUser != null && user.StateId == associatedUser.StateId)
                    };
                    selectListItems.Add(item);
                }

                // Create SelectList from the list of SelectListItem objects
                SelectList userList = new SelectList(selectListItems, "Value", "Text");

                ViewBag.UsersDropdown = userList;

                return View("UserUpdate", existingState); // Pass existing car details to the view
            }
            else
            {
                ModelState.AddModelError(string.Empty, $"Car with ID {userid} not found");
                return RedirectToAction(nameof(UserInfoIndex));
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
    public async Task<ActionResult> UserUpdate([FromRoute] int userid, [FromForm] UserInfoDto updatedUserDto)
    {
        try
        {
            var states = await _stateService.GetStateDetailsAsync();
            ViewBag.States = states ?? new List<StateDto>(); // Null check
            ViewBag.StatesCount = ViewBag.States.Count;

            if (ModelState.IsValid)
            {
                userid = updatedUserDto.Id;
                var updatedState = await _userInfoService.UpdateUserAsync(userid, updatedUserDto);

                if (updatedState != null)
                {
                    TempData["successMessage"] = "User Update successfully.";
                    return RedirectToAction(nameof(UserInfoIndex));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"User with ID {userid} not found");
                    TempData["dangerMessage"] = "User Update Failed.";
                    return View("UserUpdate", updatedUserDto);
                }
            }

            return View("UserUpdate", updatedUserDto);
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
