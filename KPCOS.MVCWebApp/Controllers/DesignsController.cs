using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;
using KPCOS.Common;
using KPCOS.Service.Base;
using Newtonsoft.Json;
using System.Text;

namespace KPCOS.MVCWebApp.Controllers
{
    public class DesignsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;
        private readonly string _apiEndpoint = Const.APIEndpoint + "Design/";

        public DesignsController(FA24_SE1717_PRN231_G4_KPCOSContext context)
        {
            _context = context;
        }

        // GET: Designs
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
                            var data = JsonConvert.DeserializeObject<List<Design>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }

            return View(new List<Design>());
        }

        // GET: Designs/Details/5
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
                            var design = JsonConvert.DeserializeObject<Design>(result.Data.ToString());
                            return View(design);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to fetch design details.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching design details.");
                }
            }

            return View(new Design());
        }

        // GET: Designs/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Name");
            return View();
        }

        // POST: Designs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Budget,ConsultantBy,CustomerId,Depth,DesignType,FiltrationSystem,KoiCountRange,KoiType,Location,MinLength,MinWidth,Note,Shape,Status,TemplateId,WaterLevel,WaterQuality")] Design design)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var jsonContent = JsonConvert.SerializeObject(design);
                        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var response = await httpClient.PostAsync(_apiEndpoint, contentString);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to create design.");
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the design.");
                    }
                }
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", design.CustomerId);
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Name", design.TemplateId);
            return View(design);
        }

        // GET: Designs/Edit/5
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
                            var design = JsonConvert.DeserializeObject<Design>(result.Data.ToString());
                            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", design.CustomerId);
                            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Name", design.TemplateId);
                            return View(design);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to fetch design for editing.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching the design for editing.");
                }
            }

            return View(new Design());
        }

        // POST: Designs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Budget,ConsultantBy,CustomerId,Depth,DesignType,FiltrationSystem,KoiCountRange,KoiType,Location,MinLength,MinWidth,Note,Shape,Status,TemplateId,WaterLevel,WaterQuality")] Design design)
        {
            if (id != design.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var jsonContent = JsonConvert.SerializeObject(design);
                        var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var response = await httpClient.PutAsync($"{_apiEndpoint}", contentString);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to update design.");
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while updating the design.");
                    }
                }
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", design.CustomerId);
            ViewData["TemplateId"] = new SelectList(_context.DesignTemplates, "Id", "Name", design.TemplateId);
            return View(design);
        }

        // GET: Designs/Delete/5
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
                            var design = JsonConvert.DeserializeObject<Design>(result.Data.ToString());
                            return View(design);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to fetch design for deletion.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching design for deletion.");
                }
            }

            return View(new Design());
        }

        // POST: Designs/Delete/5
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
                        ModelState.AddModelError(string.Empty, "Failed to delete design.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the design.");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
