using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class MakeController : Controller
    {
        private readonly IPvaMakeService _makeService;

        public MakeController(IPvaMakeService makeService)
        {
            _makeService = makeService;
        }

        public async Task<ActionResult> MakeIndex()
        {
            try
            {
                var result = await _makeService.GetMakeDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<PvaMakeDto>());
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

        public async Task<ActionResult> MakeCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> MakeCreate(PvaMakeDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _makeService.AddMakeAsync(model);

                if (result != null)
                {
                    TempData["successMessage"] = "Make Created successful.";
                    return RedirectToAction(nameof(MakeIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> MakeDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _makeService.DeleteMakeAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Make Delete successful.";
                    return RedirectToAction(nameof(MakeIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Make Delete successful.";
                    return RedirectToAction(nameof(MakeIndex));
                    return RedirectToAction(nameof(MakeIndex));
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> MakeToUpdate(int stateid)
        {
            try
            {
                var existingState = await _makeService.GetMakeByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("MakeUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(MakeIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(MakeIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> MakeUpdate([FromRoute] int stateid, [FromForm] PvaMakeDto updatedMakeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stateid = updatedMakeDto.MakeId;
                    var updatedState = await _makeService.UpdateMakeAsync(stateid, updatedMakeDto);

                    if (updatedState != null)
                    {
                        TempData["successMessage"] = "Make Update successful.";
                        
                        return RedirectToAction(nameof(MakeIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");

                        TempData["dangerMessage"] = "Make Update Unsuccessful.";

                        return RedirectToAction(nameof(MakeIndex));
                    }
                }

                return View("MakeUpdate", updatedMakeDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("MakeUpdate", updatedMakeDto);
            }
        }


    }
}