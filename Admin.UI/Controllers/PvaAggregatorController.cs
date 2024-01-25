using Admin.UI.Models;
using Admin.UI.Service;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Admin.UI.Controllers
{
    public class PvaAggregatorController : Controller
    {
        private readonly IAggregatorsService _aggregatorService;
        private readonly IUserInfoService _userService;
        private readonly IPvaMakeService _makeService;
        private readonly IPvaModelService _modelService;
        private readonly IPvaYearOfRegService _yearService;
        private readonly IPvaVariantService _variantService;

        public PvaAggregatorController(IAggregatorsService aggregatorService,
                                       IPvaVariantService variantService,
                                       IPvaYearOfRegService yearService,
                                       IPvaModelService modelService,
                                       IPvaMakeService makeService,
                                       IUserInfoService userService)
        {
            _aggregatorService = aggregatorService;
            _userService = userService;
            _variantService = variantService;
            _modelService = modelService;
            _makeService = makeService;
            _yearService = yearService;

        }

        public async Task<ActionResult> AggregatorIndex()
        {
            try
            {
                var result = await _aggregatorService.GetAggregatorDetailsAsync();

                if (result != null)
                {
                    // Assuming you have a method to get the states, replace it with your actual logic


                    var user = await _userService.GetUserDetailsAsync();
                    ViewBag.Users = user ?? new List<UserInfoDto>();

                    var make = await _makeService.GetMakeDetailsAsync();
                    ViewBag.Makes = make ?? new List<PvaMakeDto>(); // Null check

                    var model = await _modelService.GetModelDetailsAsync();
                    ViewBag.Models = model ?? new List<PvaModelDto>();

                    var year = await _yearService.GetYearDetailsAsync();
                    ViewBag.Years = year ?? new List<PvaYearOfRegDto>();

                    var variant = await _variantService.GetVariantDetailsAsync();
                    ViewBag.Variants = variant ?? new List<PvaVariantDto>();

                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<UserInfoDto>());
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



        public async Task<ActionResult> AggregatorDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _aggregatorService.DeleteAggregatorAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Delete  Successful.";

                    return RedirectToAction(nameof(AggregatorIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Delete  Unsuccessful.";
                    return RedirectToAction(nameof(AggregatorIndex));
                }
            }
            return NotFound();
        }

    }
}