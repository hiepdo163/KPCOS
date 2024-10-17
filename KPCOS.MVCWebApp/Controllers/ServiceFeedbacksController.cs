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

namespace KPCOS.MVCWebApp.Controllers
{

    public class ServiceFeedbacksController : Controller
    {
        // GET: ServiceFeedbacks
        private readonly string _apiEndpoint = Const.APIEndpoint + "ServiceFeedback/";

        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.GetAsync(_apiEndpoint))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<List<ServiceFeedback>>(result.Data.ToString());
                                return View(data);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred: " + ex.Message);
                }
            }
            return View(new List<ServiceFeedback>());
        }

        // GET: ServiceFeedbacks/Details/{id}
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            ServiceFeedback serviceFeedback = null;
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
                            serviceFeedback = JsonConvert.DeserializeObject<ServiceFeedback>(result.Data.ToString());
                        }
                    }
                }
            }

            if (serviceFeedback == null)
            {
                return NotFound();
            }

            return View(serviceFeedback);
        }

        // GET: ServiceFeedbacks/Create
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

            var serviceBookings = new List<ServiceBooking>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            serviceBookings = JsonConvert.DeserializeObject<List<ServiceBooking>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ServiceBookingId"] = new SelectList(serviceBookings, "Id", "Id");

            return View();
        }

        // POST: ServiceFeedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceBookingId,CustomerId,Rating,Feedback,CreateDate")] ServiceFeedback serviceFeedback)
        {
            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(_apiEndpoint, serviceFeedback))
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
                ViewData["CustomerId"] = new SelectList(customers, "Id", "Id");

                var serviceBookings = new List<ServiceBooking>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Data != null)
                            {
                                serviceBookings = JsonConvert.DeserializeObject<List<ServiceBooking>>(result.Data.ToString());
                            }
                        }
                    }
                }
                ViewData["ServiceBookingId"] = new SelectList(serviceBookings, "Id", "Id");

                return View(serviceFeedback);
            }
        }

        // GET: ServiceFeedbacks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            ServiceFeedback serviceFeedback = null;
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
                            serviceFeedback = JsonConvert.DeserializeObject<ServiceFeedback>(result.Data.ToString());
                        }
                    }
                }
            }

            if (serviceFeedback == null)
            {
                return NotFound();
            }

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
            ViewData["CustomerId"] = new SelectList(customers, "Id", "Id", serviceFeedback.CustomerId);

            var serviceBookings = new List<ServiceBooking>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Data != null)
                        {
                            serviceBookings = JsonConvert.DeserializeObject<List<ServiceBooking>>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ServiceBookingId"] = new SelectList(serviceBookings, "Id", "Id", serviceFeedback.ServiceBookingId);

            return View(serviceFeedback);
        }

        // POST: ServiceFeedbacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ServiceBookingId,CustomerId,Rating,Feedback,CreateDate")] ServiceFeedback serviceFeedback)
        {
            if (id != serviceFeedback.Id)
            {
                return NotFound();
            }

            bool saveStatus = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync($"{_apiEndpoint}", serviceFeedback))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                            if (result != null && result.Status == Const.SUCCESS_UPDATE_CODE)
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

        // GET: ServiceFeedbacks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            ServiceFeedback serviceFeedback = null;
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
                            serviceFeedback = JsonConvert.DeserializeObject<ServiceFeedback>(result.Data.ToString());
                        }
                    }
                }
            }

            if (serviceFeedback == null)
            {
                return NotFound();
            }

            return View(serviceFeedback);
        }

        // POST: ServiceFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool deleteStatus = false;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{_apiEndpoint}{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                        if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                        {
                            deleteStatus = true;
                        }
                        else
                        {
                            deleteStatus = false;
                        }
                    }
                }
            }
            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Delete", id);
            }
        }
    }
}