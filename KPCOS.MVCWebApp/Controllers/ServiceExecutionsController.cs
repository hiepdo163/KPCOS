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
using KPCOS.Service.DTOs;

namespace KPCOS.MVCWebApp.Controllers
{
    public class ServiceExecutionsController : Controller
    {      
        // GET: ServiceExecutions
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceExecution"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<ServiceExecution>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ServiceExecution>());
            //return View();
        }

        // GET: ServiceExecutions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceExecution/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceExecution>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceExecution());
        }

        // GET: ServiceExecutions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType");
            ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname");
            return View();
        }

        // POST: ServiceExecutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceExecutionDTO serviceExecution)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + "ServiceExecution", serviceExecution))
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
                ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType", serviceExecution.ServiceBookingId);
                ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname", serviceExecution.EmployeeId);
                return View(serviceExecution);
            }
        }

        // GET: ServiceExecutions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var serviceExecution = new ServiceExecution();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceExecution/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            serviceExecution = JsonConvert.DeserializeObject<ServiceExecution>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType", serviceExecution.ServiceBookingId);
            ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname", serviceExecution.EmployeeId);
            return View(serviceExecution);
        }

        // POST: ServiceExecutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ServiceExecutionDTO serviceExecution)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndpoint + "ServiceExecution/" + id, serviceExecution))
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
                ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType", serviceExecution.ServiceBookingId);
                ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname", serviceExecution.EmployeeId);
                return View(serviceExecution);
            }
        }

        // GET: ServiceExecutions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceExecution/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceExecution>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceExecution());
        }

        // POST: ServiceExecutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndpoint + "ServiceExecution/delete/" + id))
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

        public async Task<List<ServiceBooking>> GetServiceBookings()
        {
            var bookings = new List<ServiceBooking>();
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
                            bookings = JsonConvert.DeserializeObject<List<ServiceBooking>>(result.Data.ToString());
                        }
                    }
                }
            }
            return bookings;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = new List<Employee>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Employee"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            employees = JsonConvert.DeserializeObject<List<Employee>>(result.Data.ToString());
                        }
                    }
                }
            }
            return employees;
        }
    }
}
