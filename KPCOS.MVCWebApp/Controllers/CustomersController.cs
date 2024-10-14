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
    public class CustomersController(FA24_SE1717_PRN231_G4_KPCOSContext context) : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context = context;

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            //var fA24_SE1717_PRN231_G4_KPCOSContext = _context.Customers.Include(c => c.Package).Include(c => c.User);
            //return View(await fA24_SE1717_PRN231_G4_KPCOSContext.ToListAsync());
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
                            var data = JsonConvert.DeserializeObject<List<Customer>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Customer>());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var customer = await _context.Customers
            //    .Include(c => c.Package)
            //    .Include(c => c.User)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (customer == null)
            //{
            //    return NotFound();
            //}

            //return View(customer);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Customer());
        }

        // GET: Customers/Create
        //public IActionResult Create()
        //{
        //    ViewData["PackageId"] = new SelectList(_context.Packages, "Id", "Id");
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "Id", "Fullname");
            ViewData["PackageId"] = new SelectList(await this.GetPackages(), "Id", "Description");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(customer);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PackageId"] = new SelectList(_context.Packages, "Id", "Id", customer.PackageId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customer.UserId);
            //return View(customer);

            bool Status = false;
            #region Create customer
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + "Customer", customer))
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
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "Id", "Fullname", customer.UserId);
                ViewData["PackageId"] = new SelectList(await this.GetPackages(), "Id", "Description", customer.PackageId);
                return View(customer);
            }
            #endregion
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var customer = await _context.Customers.FindAsync(id);
            //if (customer == null)
            //{
            //    return NotFound();
            //}
            //ViewData["PackageId"] = new SelectList(_context.Packages, "Id", "Id", customer.PackageId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customer.UserId);
            //return View(customer);
            var customer = new Customer();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            customer = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                        }
                    }
                }
            }
            ViewData["UserId"] = new SelectList(await this.GetUsers(), "Id", "Fullname", customer.UserId);
            ViewData["PackageId"] = new SelectList(await this.GetPackages(), "Id", "Description", customer.PackageId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            //if (id != customer.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(customer);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CustomerExists(customer.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PackageId"] = new SelectList(_context.Packages, "Id", "Id", customer.PackageId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customer.UserId);
            //return View(customer);

            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndpoint + "Customer", customer))
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
                ViewData["UserId"] = new SelectList(await this.GetUsers(), "Id", "Fullname", customer.UserId);
                ViewData["PackageId"] = new SelectList(await this.GetPackages(), "Id", "Description", customer.PackageId);
                return View(customer);
            }
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var customer = await _context.Customers
            //    .Include(c => c.Package)
            //    .Include(c => c.User)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (customer == null)
            //{
            //    return NotFound();
            //}

            //return View(customer);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Customer/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Customer>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Customer());
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //var customer = await _context.Customers.FindAsync(id);
            //if (customer != null)
            //{
            //    _context.Customers.Remove(customer);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndpoint + "Customer/" + id))
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
                return RedirectToAction(nameof(Delete));
            }
        }

        //private bool CustomerExists(string id)
        //{
        //    return _context.Customers.Any(e => e.Id == id);
        //}

        public async Task<List<User>> GetUsers()
        {
            var users = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "User"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            users = JsonConvert.DeserializeObject<List<User>>(result.Data.ToString());
                        }
                    }
                }
            }
            return users;
        }

        public async Task<List<Package>> GetPackages()
        {
            var packages = new List<Package>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + "Package"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            packages = JsonConvert.DeserializeObject<List<Package>>(result.Data.ToString());
                        }
                    }
                }
            }
            return packages;
        }
    }
}
