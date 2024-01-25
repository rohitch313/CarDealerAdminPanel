using Admin.UI.Models;
using Admin.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Admin.UI.Controllers
{
    public class CustomerSupportController : Controller
    {
        private readonly ICustomerSupportService _customerService;

        public CustomerSupportController(ICustomerSupportService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ActionResult> CustomerIndex()
        {
            try
            {
                var result = await _customerService.GetCrustSptDetailsAsync();

                if (result != null)
                {
                    return View(result);
                }

                // Handle the case where result is null, e.g., return an empty view or show an error message
                return View(new List<CustomerSupportDto>());
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

        public async Task<ActionResult> CustomerCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CustomerCreate(CustomerSupportDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.AddCrustSptAsync(model);

                if (result != null)
                {
                    TempData["successMessage"] = "Customer Support Created  Successfully.";
                    return RedirectToAction(nameof(CustomerIndex));
                }
            }
            return View(model);
        }

        public async Task<ActionResult> CustomerDelete(int stateid)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.DeleteCrustSptAsync(stateid);

                if (result != null)
                {
                    TempData["successMessage"] = "Customer Support Deleted Successfully.";
                    return RedirectToAction(nameof(CustomerIndex));
                }
                else
                {
                    // Handle 404 Not Found
                    ModelState.AddModelError(string.Empty, "The requested state was not found.");
                    TempData["dangerMessage"] = "Customer Support Delete  Unsuccessfully.";
                    return RedirectToAction(nameof(CustomerIndex));
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> CustomerToUpdate(int stateid)
        {
            try
            {
                var existingState = await _customerService.GetCrustSptByIdAsync(stateid);

                if (existingState != null)
                {
                    return View("CustomerUpdate", existingState); // Pass existing state to the view
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                    return RedirectToAction(nameof(CustomerIndex));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return RedirectToAction(nameof(CustomerIndex));
            }
        }

        [HttpPost]
        public async Task<ActionResult> CustomerUpdate([FromRoute] int stateid, [FromForm] CustomerSupportDto updatedCustomerDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    stateid = updatedCustomerDto.Id;
                    var updatedState = await _customerService.UpdateCrustSptAsync(stateid, updatedCustomerDto);

                    if (updatedState != null)
                    {
                        TempData["successMessage"] = "Customer Support Updated  Successfully.";
                        return RedirectToAction(nameof(CustomerIndex));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"State with ID {stateid} not found");
                        TempData["dangerMessage"] = "Customer Support Update  Failed.";
                        return View("CustomerUpdate", updatedCustomerDto);
                    }
                }

                return View("CustomerUpdate", updatedCustomerDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                ModelState.AddModelError(string.Empty, "Internal Server Error");
                return View("CustomerUpdate", updatedCustomerDto);
            }
        }


    }
}
