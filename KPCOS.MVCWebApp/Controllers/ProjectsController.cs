using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KPCOS.Data.Models;
using KPCOS.Common;
using KPCOS.Service.Base;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;

namespace KPCOS.MVCWebApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly string _apiEndpoint = Const.APIEndpoint + "Project/";
        private readonly string _apiEmployeeEndpoint = Const.APIEndpoint + "Employee/";
        private readonly string _apiCustomerEndpoint = Const.APIEndpoint + "Customer/";

        private async Task<IEnumerable<Employee>> GetConstructionStaffAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_apiEmployeeEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result?.Data != null)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<Employee>>(result.Data.ToString());
                    }
                }
            }
            return Enumerable.Empty<Employee>();
        }

        private async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_apiCustomerEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result?.Data != null)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<Customer>>(result.Data.ToString());
                    }
                }
            }
            return Enumerable.Empty<Customer>();
        }

        private async Task<IEnumerable<Employee>> GetDesignersAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_apiEmployeeEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result?.Data != null)
                    {
                        return JsonConvert.DeserializeObject<IEnumerable<Employee>>(result.Data.ToString());
                    }
                }
            }
            return Enumerable.Empty<Employee>();
        }


        // GET: Projects
        public async Task<IActionResult> Index()
        {
            List<Project> projects = new();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(_apiEndpoint);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result?.Data != null)
                        {
                            projects = JsonConvert.DeserializeObject<List<Project>>(result.Data.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }

            Debug.WriteLine($"Number of projects: {projects.Count}");
            return View(projects);
        }


        // GET: Projects/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            Project project = null;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_apiEndpoint}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result?.Data != null)
                    {
                        project = JsonConvert.DeserializeObject<Project>(result.Data.ToString());
                    }
                }
            }

            if (project == null) return NotFound();
            return View(project);
        }

        // GET: Projects/Create
        public async Task<IActionResult> Create()
        {
            var constructionStaffList = (await GetConstructionStaffAsync()).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.User.Fullname,
            }).ToList();
            ViewBag.ConstructionStaffId = new SelectList(constructionStaffList, "Value", "Text");

            var customerList = (await GetCustomersAsync()).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.User.Fullname,
            }).ToList();
            ViewBag.CustomerId = new SelectList(customerList, "Value", "Text");

            var designerList = (await GetDesignersAsync()).Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.User.Fullname,
            }).ToList();
            ViewBag.DesignerId = new SelectList(designerList, "Value", "Text");

            return View();
        }


        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActualCost,ConstructionStaffId,CustomerId,DesignerId,StartDate,EndDate,EstimatedCost,Status")] Project project)
        {
            if (!ModelState.IsValid) return View(project);

            bool isSuccess = false;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(_apiEndpoint, project);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    isSuccess = result?.Status == Const.SUCCESS_CREATE_CODE;
                }
            }

            return isSuccess ? RedirectToAction(nameof(Index)) : View(project);
        }

        // GET: Projects/Edit/{id}
        public async Task<IActionResult> Edit(String id)
        {
            if (id == String.Empty) return NotFound();

            Project project = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_apiEndpoint}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result?.Data != null)
                    {
                        project = JsonConvert.DeserializeObject<Project>(result.Data.ToString());
                    }
                }
            }

            if (project == null) return NotFound();

            ViewBag.ConstructionStaffId = new SelectList(await GetConstructionStaffAsync(), "User.Id", "User.Fullname", project.ConstructionStaffId);
            ViewBag.CustomerId = new SelectList(await GetCustomersAsync(), "Id", "User.Fullname", project.CustomerId);
            ViewBag.DesignerId = new SelectList(await GetDesignersAsync(), "Id", "User.Fullname", project.DesignerId);

            return View(project);
        }


        // POST: Projects/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(String id, [Bind("Id,ActualCost,ConstructionStaffId,CustomerId,DesignerId,StartDate,EndDate,EstimatedCost,Status")] Project project)
        {
            if (id != project.Id) return NotFound();

            if (!ModelState.IsValid) return View(project);

            bool isSuccess = false;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(_apiEndpoint, project);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    isSuccess = result?.Status == Const.SUCCESS_UPDATE_CODE;
                }
            }

            return isSuccess ? RedirectToAction(nameof(Index)) : View(project);
        }

        // GET: Projects/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty) return NotFound();

            Project project = null;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_apiEndpoint}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    if (result?.Data != null)
                    {
                        project = JsonConvert.DeserializeObject<Project>(result.Data.ToString());
                    }
                }
            }

            if (project == null) return NotFound();
            return View(project);
        }

        // POST: Projects/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            bool isDeleted = false;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"{_apiEndpoint}{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);
                    isDeleted = result?.Status == Const.SUCCESS_DELETE_CODE;
                }
            }

            return isDeleted ? RedirectToAction(nameof(Index)) : View("Delete", id);
        }
    }
}
