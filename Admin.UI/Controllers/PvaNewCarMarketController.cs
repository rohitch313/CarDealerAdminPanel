using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class PvaNewCarMarketController : Controller
    {
        private readonly IPvNewCarDealersService _newCarService;

        public PvaNewCarMarketController(IPvNewCarDealersService newCarService)
        {
            _newCarService = newCarService;
        }

        public async Task<ActionResult> NewCarIndex()
        {
            try
            {
                var result = await _newCarService.GetNewCarDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<PvNewCarDealersDto>());
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



        public async Task<ActionResult> NewCarDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _newCarService.DeleteNewCarAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Delete  Successful.";
                    return RedirectToAction(nameof(NewCarIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Delete  Unsuccessful.";
                    return RedirectToAction(nameof(NewCarIndex));
                }
            }
            return NotFound();
        }

    }
}