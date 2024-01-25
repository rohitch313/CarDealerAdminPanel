using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class PvaOpenMarketController : Controller
    {
        private readonly IPvOpenMarketsService _openService;

        public PvaOpenMarketController(IPvOpenMarketsService openService)
        {
            _openService = openService;
        }

        public async Task<ActionResult> OpenMarketIndex()
        {
            try
            {
                var result = await _openService.GetOpenMarketDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<PvOpenMarketsDto>());
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



        public async Task<ActionResult> OpenMarketDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _openService.DeleteOpenMarketAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Delete  Successful.";
                    return RedirectToAction(nameof(OpenMarketIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Delete  Unsuccessful.";
                    return RedirectToAction(nameof(OpenMarketIndex));
                }
            }
            return NotFound();
        }

    }
}