using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class ModelController : Controller
    {
        private readonly IPvaModelService _modelService;

        public ModelController(IPvaModelService modelService)
        {
            _modelService = modelService;
        }

        public async Task<ActionResult> ModelIndex()
        {
            try
            {
                var result = await _modelService.GetModelDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<PvaModelDto>());
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

        public async Task<ActionResult> ModelCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ModelCreate(PvaModelDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _modelService.AddModelAsync(model);

                if (result != null)
                {
                    TempData["successMessage"] = "Model Create successful.";

                    return RedirectToAction(nameof(ModelIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> ModelDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _modelService.DeleteModelAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Model Delete successful.";

                    
                    return RedirectToAction(nameof(ModelIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Model Delete Unsuccessful.";
                    return RedirectToAction(nameof(ModelIndex));
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> ModelToUpdate(int stateid)
        {
            try
            {
                var existingState = await _modelService.GetModelByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("ModelUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(ModelIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(ModelIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> ModelUpdate([FromRoute] int stateid, [FromForm] PvaModelDto updatedModelDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stateid = updatedModelDto.ModelId;
                    var updatedState = await _modelService.UpdateModelAsync(stateid, updatedModelDto);

                    if (updatedState != null)
                    {
                        TempData["successMessage"] = "Model Update successful.";
                        return RedirectToAction(nameof(ModelIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                        TempData["dangerMessage"] = "Model Update Unsuccessful.";
                        return RedirectToAction(nameof(ModelIndex));
                    }
                }

                return View("ModelUpdate", updatedModelDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("ModelUpdate", updatedModelDto);
            }
        }


    }
}