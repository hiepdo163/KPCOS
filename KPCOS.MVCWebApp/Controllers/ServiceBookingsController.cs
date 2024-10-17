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
    public class ServiceBookingsController : Controller
    {
        // GET: ServiceBookings
        public async Task<IActionResult> Index()
        {
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
                            var data = JsonConvert.DeserializeObject<List<ServiceBooking>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ServiceBooking>());
            //return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceBooking>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceBooking());
        }
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await this.GetCustomers(), "Id", "User.Fullname");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceBookingDTO serviceBookingDTO)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + "ServiceBooking", serviceBookingDTO))
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
                                //return View(customer);
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
                ViewData["CustomerId"] = new SelectList(await this.GetCustomers(), "Id", "User.Fullname", serviceBookingDTO.CustomerId);
                return View(serviceBookingDTO);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var serviceBooking = new ServiceBooking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            serviceBooking = JsonConvert.DeserializeObject<ServiceBooking>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["CustomerId"] = new SelectList(await this.GetCustomers(), "Id", "User.Fullname", serviceBooking.CustomerId);
            return View(serviceBooking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ServiceBookingDTO serviceBookingDTO)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndpoint + "ServiceBooking/" + id, serviceBookingDTO))
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
                                //return View(customer);
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
                ViewData["CustomerId"] = new SelectList(await this.GetCustomers(), "Id", "User.Fullname", serviceBookingDTO.CustomerId);
                return View(serviceBookingDTO);
            }
        }

        // GET: ServiceAssignments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceBooking>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceBooking());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndpoint + "ServiceBooking/delete/" + id))
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

        public async Task<List<Customer>> GetCustomers()
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
            return customers;
        }
    }
}
