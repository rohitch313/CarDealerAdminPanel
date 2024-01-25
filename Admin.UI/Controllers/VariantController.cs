using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class VariantController : Controller
    {
        private readonly IPvaVariantService _variantService;

        public VariantController(IPvaVariantService variantService)
        {
            _variantService = variantService;
        }

        public async Task<ActionResult> VariantIndex()
        {
            try
            {
                var result = await _variantService.GetVariantDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<PvaVariantDto>());
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

        public async Task<ActionResult> VariantCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> VariantCreate(PvaVariantDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _variantService.AddVariantAsync(model);

                if (result != null)
                {
                    TempData["successMessage"] = "Variant Created  Successfully.";
                    return RedirectToAction(nameof(VariantIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> MakeDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _variantService.DeleteVariantAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Variant Delete  Successfully.";
                    return RedirectToAction(nameof(VariantIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Variant Delete  Failed.";
                    return RedirectToAction(nameof(VariantIndex));
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> VariantToUpdate(int stateid)
        {
            try
            {
                var existingState = await _variantService.GetVariantByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("VariantUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(VariantIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(VariantIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> VariantUpdate([FromRoute] int stateid, [FromForm] PvaVariantDto updatedVariantDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stateid = updatedVariantDto.VariantId;
                    var updatedState = await _variantService.UpdateVariantAsync(stateid, updatedVariantDto);

                    if (updatedState != null)
                    {
                        TempData["successMessage"] = "Variant Updated  Successfully.";
                        return RedirectToAction(nameof(VariantIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                        TempData["dangerMessage"] = "Variant Update  Failed.";
                        return RedirectToAction(nameof(VariantIndex));
                    }
                }

                return View("VariantUpdate", updatedVariantDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("VariantUpdate", updatedVariantDto);
            }
        }


    }
}