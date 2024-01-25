using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class YearOfRegController : Controller
    {
        private readonly IPvaYearOfRegService _yearService;

        public YearOfRegController(IPvaYearOfRegService yearService)
        {
            _yearService = yearService;
        }

        public async Task<ActionResult> YearIndex()
        {
            try
            {
                var result = await _yearService.GetYearDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<PvaYearOfRegDto>());
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
                throw; // rethrow the exception to propagate it up the call stack
            }
        }

        public async Task<ActionResult> YearCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> YearCreate(PvaYearOfRegDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _yearService.AddYearAsync(model);

                if (result != null)
                {
                    TempData["successMessage"] = "Year Created  Successfully.";
                    return RedirectToAction(nameof(YearIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> YearDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _yearService.DeleteYearAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Year Deleted  Successfully.";
                    return RedirectToAction(nameof(YearIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Year Deleted  Unsuccessfully.";
                    return RedirectToAction(nameof(YearIndex));
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> YearToUpdate(int stateid)
        {
            try
            {
                var existingState = await _yearService.GetYearByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("YearUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(YearIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(YearIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> YearUpdate([FromRoute] int stateid, [FromForm] PvaYearOfRegDto updatedYearDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stateid = updatedYearDto.YearId;
                    var updatedState = await _yearService.UpdateYearAsync(stateid, updatedYearDto);

                    if (updatedState != null)
                    {
                        TempData["successMessage"] = "Year Updated  Successfully.";
                        return RedirectToAction(nameof(YearIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                        TempData["dangerMessage"] = "Year Updated  Unsuccessfully.";
                        return View("YearUpdate", updatedYearDto);
                    }
                }

                return View("YearUpdate", updatedYearDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("YearUpdate", updatedYearDto);
            }
        }


    }
}