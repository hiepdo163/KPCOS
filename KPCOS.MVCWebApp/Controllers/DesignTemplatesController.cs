using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;
using KPCOS.Common;
using Newtonsoft.Json;
using KPCOS.Service.Base;
using System.Text;

namespace KPCOS.MVCWebApp.Controllers
{
    public class DesignTemplatesController : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;
        private readonly string _apiEndpoint = Const.APIEndpoint + "DesignTemplate/";

        public DesignTemplatesController(FA24_SE1717_PRN231_G4_KPCOSContext context)
        {
            _context = context;
        }

        // GET: DesignTemplates
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_apiEndpoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<DesignTemplate>>(result.Data.ToString());

                            return View(data);
                        }
                    }
                }
            }

            return View(new List<DesignTemplate>());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"{_apiEndpoint}{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var designTemplate = JsonConvert.DeserializeObject<DesignTemplate>(result.Data.ToString());
                            return View(designTemplate);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to fetch design template details.");
                    }
                }
                catch (Exception ex)
                {
                    // Log error (ex) and add error message to ModelState
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching design template details.");
                }
            }

            return View(new DesignTemplate());
        }

        // GET: DesignTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DesignTemplates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DefaultLocation,DefaultShape,DefaultSize,Description,Image,Name,TotalPrice")] DesignTemplate designTemplate)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // Convert the design template to JSON
                        var jsonContent = JsonConvert.SerializeObject(designTemplate);
                        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        // Post the data to the API
                        var response = await httpClient.PostAsync(Const.APIEndpoint + "DesignTemplate", contentString);

                        // Check if the response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to create design template.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error and show a relevant error message
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the design template.");
                    }
                }
            }

            // If the ModelState is invalid, return the view with validation errors
            return View(designTemplate);
        }

        // GET: DesignTemplates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"{_apiEndpoint}{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var designTemplate = JsonConvert.DeserializeObject<DesignTemplate>(result.Data.ToString());
                            return View(designTemplate);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to fetch design template for editing.");
                    }
                }
                catch (Exception ex)
                {
                    // Log error and add error message to ModelState
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching the design template for editing.");
                }
            }

            return View(new DesignTemplate());
        }

        // POST: DesignTemplates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DefaultLocation,DefaultShape,DefaultSize,Description,Image,Name,TotalPrice")] DesignTemplate designTemplate)
        {
            if (id != designTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        // Convert the edited design template to JSON
                        var jsonContent = JsonConvert.SerializeObject(designTemplate);
                        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        // Make the API call to update the design template
                        var response = await httpClient.PutAsync($"{_apiEndpoint}", contentString);

                        // Check if the response is successful
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to update design template.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error and show a relevant error message
                        ModelState.AddModelError(string.Empty, "An error occurred while updating the design template.");
                    }
                }
            }

            return View(designTemplate);
        }

        // GET: DesignTemplates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"{_apiEndpoint}{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var designTemplate = JsonConvert.DeserializeObject<DesignTemplate>(result.Data.ToString());
                            return View(designTemplate);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to fetch design template for deletion.");
                    }
                }
                catch (Exception ex)
                {
                    // Log error (ex) and add error message to ModelState
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching design template for deletion.");
                }
            }

            return View(new DesignTemplate());
        }

        // POST: DesignTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.DeleteAsync($"{_apiEndpoint}{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to delete design template.");
                    }
                }
                catch (Exception ex)
                {
                    // Log error (ex) and add error message to ModelState
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the design template.");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
