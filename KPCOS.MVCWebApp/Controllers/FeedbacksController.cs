﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;
using KPCOS.Common;
using KPCOS.Service.Base;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using KPCOS.Service.DTOs;


namespace KPCOS.MVCWebApp.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly string _apiEndpoint = Const.APIEndpoint + "Feedback/";
        // GET: Feedbacks
        public async Task<IActionResult> Index(string content, decimal? rating, DateTime? createdDate)
        {
            List<Feedback> data = new List<Feedback>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + nameof(Feedback)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(responseContent);

                        if (result != null && result.Data != null)
                        {
                            data = JsonConvert.DeserializeObject<List<Feedback>>(result.Data.ToString());
                        }
                    }
                }
            }

            // Filter by content
            if (!string.IsNullOrEmpty(content))
            {
                data = data.Where(c => c.Content.Contains(content, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Filter by rating
            if (rating.HasValue)
            {
                data = data.Where(c => c.Rating >= rating.Value).ToList();
            }

            // Filter by createdDate
            if (createdDate.HasValue)
            {
                data = data.Where(c => c.CreateDate.HasValue && c.CreateDate.Value.Date == createdDate.Value.Date).ToList();
            }

            return View(data);
        }


        // GET: Feedbacks/Details/{id}

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Feedback feedback = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_apiEndpoint }{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            feedback = JsonConvert.DeserializeObject<Feedback>(result.Data.ToString());
                        }
                    }
                }
            }

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // GET: Feedbacks/Create
        public async Task<IActionResult> Create()
        {
            var customers = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            customers = JsonConvert.DeserializeObject<List<Customer>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["CustomerId"] = new SelectList(customers, "Id", "Id");

            var projects = new List<Project>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Project"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            projects = JsonConvert.DeserializeObject<List<Project>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ProjectId"] = new SelectList(projects, "Id", "Id");

            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,CustomerId,ProjectId,Rating")] Feedback feedback)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_apiEndpoint, feedback))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var customers = new List<Customer>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                customers = JsonConvert.DeserializeObject<List<Customer>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["CustomerId"] = new SelectList(customers, "Id", "Id", feedback.CustomerId);

                var projects = new List<Project>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Project"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                projects = JsonConvert.DeserializeObject<List<Project>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ProjectId"] = new SelectList(projects, "Id", "Id", feedback.ProjectId);
                return View(feedback);
            }
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Feedback feedback = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_apiEndpoint}{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            feedback = JsonConvert.DeserializeObject<Feedback>(result.Data.ToString());
                        }
                    }
                }
            }

            if (feedback == null)
            {
                return NotFound();
            }

            // Get dropdown data ------------------------------------------------------------
            var customers = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var customerData = result.Data.ToString();
                            if (!string.IsNullOrEmpty(customerData))
                            {
                                customers = JsonConvert.DeserializeObject<List<Customer>>(customerData);
                            }
                        }
                    }
                }
            }

            if (customers != null && customers.Count > 0)
            {
                ViewData["CustomerId"] = new SelectList(customers, "Id", "Id", feedback.CustomerId);
                ViewBag.CustomerId = ViewData["CustomerId"];
            }
            else
            {
                ViewData["Id"] = new SelectList(new List<Customer>(), "Id", "Id");
                ViewBag.CustomerId = ViewData["CustomerId"];
            }

            var projects = new List<Project>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Project"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            var projectData = result.Data.ToString();
                            if (!string.IsNullOrEmpty(projectData))
                            {
                                projects = JsonConvert.DeserializeObject<List<Project>>(projectData);
                            }
                        }
                    }
                }
            }

            if (projects != null && projects.Count > 0)
            {
                ViewData["ProjectId"] = new SelectList(projects, "Id", "Id", feedback.ProjectId);
                ViewBag.ProjectId = ViewData["ProjectId"];
            }
            else
            {
                ViewData["ProjectId"] = new SelectList(new List<Project>(), "Id", "Id");
                ViewBag.ProjectId = ViewData["ProjectId"];
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content,CustomerId,ProjectId,Rating")] Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return NotFound();
            }

            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync($"{_apiEndpoint}", feedback))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_CREATE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(id);
            }
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Feedback feedback = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_apiEndpoint}{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            feedback = JsonConvert.DeserializeObject<Feedback>(result.Data.ToString());
                        }
                    }
                }
            }

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync($"{_apiEndpoint}{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status  == Const.SUCCESS_DELETE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }
            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

    }




}