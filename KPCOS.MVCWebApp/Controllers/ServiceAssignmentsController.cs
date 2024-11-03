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
    public class ServiceAssignmentsController : Controller
    {
        // GET: ServiceAssignments
        public async Task<IActionResult> Index(QueryPagedServiceAssignment query)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceAssignment?ServiceBookingId="+ query.ServiceBookingId +"&EmployeeId="+ query.EmployeeId +"&Status="+ query.Status + "&PageNumber=" + query.PageNumber))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<PagedResultResponse<ServiceAssignment>>(result.Data.ToString());
                            ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType");
                            ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname");
                            return View(data);
                        }
                    }
                }
            }
            return View(new PagedResultResponse<ServiceAssignment>());
            //return View();
        }

        // GET: ServiceAssignments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceAssignment/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceAssignment>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceAssignment());
        }

        // GET: ServiceAssignments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType");
            ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname");
            return View();
        }

        // POST: ServiceAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceAssignmentDTO serviceAssignment)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + "ServiceAssignment", serviceAssignment))
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
                ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType", serviceAssignment.ServiceBookingId);
                ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname", serviceAssignment.EmployeeId);
                return View(serviceAssignment);
            }
        }

        // GET: ServiceAssignments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var serviceAssignment = new ServiceAssignment();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceAssignment/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            serviceAssignment = JsonConvert.DeserializeObject<ServiceAssignment>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType", serviceAssignment.ServiceBookingId);
            ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname", serviceAssignment.EmployeeId);
            return View(serviceAssignment);
        }

        // POST: ServiceAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ServiceAssignmentDTO serviceAssignment)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndpoint + "ServiceAssignment/" + id, serviceAssignment))
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
                ViewData["ServiceBookingId"] = new SelectList(await this.GetServiceBookings(), "Id", "ServiceType", serviceAssignment.ServiceBookingId);
                ViewData["EmployeeId"] = new SelectList(await this.GetEmployees(), "Id", "User.Fullname", serviceAssignment.EmployeeId);
                return View(serviceAssignment);
            }
        }

        // GET: ServiceAssignments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceAssignment/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ServiceAssignment>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new ServiceAssignment());
        }

        // POST: ServiceAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndpoint + "ServiceAssignment/delete/" + id))
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
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "ServiceBooking/get-all"))
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
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Employee/get-all"))
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
