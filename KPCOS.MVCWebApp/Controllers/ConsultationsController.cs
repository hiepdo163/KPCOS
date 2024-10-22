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
using KPCOS.Service.DTOs;

namespace KPCOS.MVCWebApp.Controllers
{
    public class ConsultationsController : Controller
    {
        // GET: Consultations
        public async Task<IActionResult> Index(string searchAdjustedDesign, string searchAdjustedSpecification, string searchNote)
        {
            List<Consultation> data = new List<Consultation>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + nameof(Consultation)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            data = JsonConvert.DeserializeObject<List<Consultation>>(result.Data.ToString());
                        }
                    }
                }
            }

            // Filter by search string if provided
            if (!string.IsNullOrEmpty(searchAdjustedDesign))
            {
                data = data.Where(c => c.AdjustedDesign.Contains(searchAdjustedDesign, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(searchAdjustedSpecification))
            {
                data = data.Where(c => c.AdjustedSpecification.Contains(searchAdjustedSpecification, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(searchNote))
            {
                data = data.Where(c => c.Note.Contains(searchNote, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(data);
        }


        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Consultation)}/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Consultation>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Consultation());
        }

        // GET: Consultations/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DesignId"] = new SelectList(await this.GetDesign(), "Id", "Id");
            return View();
        }

        public async Task<List<Design>> GetDesign()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Design)}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Design>>(result.Data.ToString());
                            return data;
                        }
                    }
                }
            }
            return new List<Design>();
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdjustedDesign,AdjustedSpecification,DesignId,Note")] ConsultationDTO consultation)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    Consultation requestModel = new Consultation()
                    {
                        Id = Guid.NewGuid().ToString(),
                        AdjustedDesign = consultation.AdjustedDesign,
                        AdjustedSpecification = consultation.AdjustedSpecification,
                        DesignId = consultation.DesignId,
                        Note = consultation.Note
                    };
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + $"{nameof(Consultation)}", requestModel))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                Status = true;
                            }
                            else
                            {
                                Status = false;
                            }
                        }
                    }
                }
            }
            if (Status)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["DesignId"] = new SelectList(await this.GetDesign(), "Id", "Id");
                return View(consultation);
            }
        }

        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var consulation = new Consultation();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Consultation)}/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            consulation = JsonConvert.DeserializeObject<Consultation>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["DesignId"] = new SelectList(await this.GetDesign(), "Id", "Id");
            return View(consulation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AdjustedDesign,AdjustedSpecification,DesignId,Note")] Consultation consultation)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndpoint + $"{nameof(Consultation)}", consultation))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
                            {
                                status = true;
                            }
                            else
                            {
                                status = false;
                            }
                        }
                    }
                }
            }
            if (status)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["DesignId"] = new SelectList(await this.GetDesign(), "Id", "Id");
                return View(consultation);
            }
        }

        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Consultation)}/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Consultation>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Consultation());
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndpoint + $"{nameof(Consultation)}/" + id))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                status = true;
                            }
                            else
                            {
                                status = false;
                            }
                        }
                    }
                }
            }
            if (status)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}
