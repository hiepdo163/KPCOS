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
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + nameof(Invoice)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Invoice>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Invoice>());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Invoice)}/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Invoice>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Invoice());
        }

        // GET: Invoice/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProjectId"] = new SelectList(await GetProject(), "Id", "Id");

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pending", Text = "Pending" },
                new SelectListItem { Value = "Paid", Text = "Paid" },
                new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
            };

            ViewBag.PaymentMethodList = new List<SelectListItem>
            {
                new SelectListItem { Value = "CreditCard", Text = "Credit Card" },
                new SelectListItem { Value = "BankTransfer", Text = "Bank Transfer" },
                new SelectListItem { Value = "Cash", Text = "Cash" }
            };

            return View();
        }

        public async Task<List<Project>> GetProject()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Project)}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Project>>(result.Data.ToString());
                            return data;
                        }
                    }
                }
            }
            return new List<Project>();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiscountApplied,PaymentDate,PaymentMethod,ProjectId,Status,TaxAmount,TotalAmount")] Invoice invoice)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                if (invoice.DiscountApplied < 0 || invoice.DiscountApplied > invoice.TotalAmount)
                {
                    ModelState.AddModelError("DiscountApplied", "Discount cannot be negative or exceed total amount");
                    await LoadInvoiceViewData();
                    return View(invoice);
                }

                if (invoice.TaxAmount < 0 || invoice.TaxAmount > invoice.TotalAmount)
                {
                    ModelState.AddModelError("TaxAmount", "Tax amount cannot be negative or exceed total amount");
                    await LoadInvoiceViewData();
                    return View(invoice);
                }

                if (invoice.TotalAmount < 0)
                {
                    ModelState.AddModelError("TotalAmount", "Total amount cannot be negative");
                    await LoadInvoiceViewData();
                    return View(invoice);
                }

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.APIEndpoint + $"{nameof(Invoice)}", invoice))
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
                await LoadInvoiceViewData();
                return View(invoice);
            }
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var invoice = new Invoice();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Invoice)}/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            invoice = JsonConvert.DeserializeObject<Invoice>(result.Data.ToString());
                        }
                    }
                }
            }

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pending", Text = "Pending" },
                new SelectListItem { Value = "Paid", Text = "Paid" },
                new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
            };

            ViewBag.PaymentMethodList = new List<SelectListItem>
            {
                new SelectListItem { Value = "CreditCard", Text = "Credit Card" },
                new SelectListItem { Value = "BankTransfer", Text = "Bank Transfer" },
                new SelectListItem { Value = "Cash", Text = "Cash" }
            };

            ViewData["ProjectId"] = new SelectList(await this.GetProject(), "Id", "Id");
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DiscountApplied,PaymentDate,PaymentMethod,ProjectId,Status,TaxAmount,TotalAmount")] Invoice invoice)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (invoice.DiscountApplied < 0 || invoice.DiscountApplied > invoice.TotalAmount)
                {
                    ModelState.AddModelError("DiscountApplied", "Discount cannot be negative or exceed total amount");
                    await LoadInvoiceViewData();
                    return View(invoice);
                }

                if (invoice.TaxAmount < 0 || invoice.TaxAmount > invoice.TotalAmount)
                {
                    ModelState.AddModelError("TaxAmount", "Tax amount cannot be negative or exceed total amount");
                    await LoadInvoiceViewData();
                    return View(invoice);
                }

                if (invoice.TotalAmount < 0)
                {
                    ModelState.AddModelError("TotalAmount", "Total amount cannot be negative");
                    await LoadInvoiceViewData();
                    return View(invoice);
                }

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.APIEndpoint + $"{nameof(Invoice)}", invoice))
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
                await LoadInvoiceViewData();
                return View(invoice);
            }
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndpoint + $"{nameof(Invoice)}/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Invoice>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new Invoice());
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync(Const.APIEndpoint + $"{nameof(Invoice)}/" + id))
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

        private async Task LoadInvoiceViewData()
        {
            ViewData["ProjectId"] = new SelectList(await this.GetProject(), "Id", "Id");

            ViewBag.StatusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pending", Text = "Pending" },
                new SelectListItem { Value = "Paid", Text = "Paid" },
                new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
            };

            ViewBag.PaymentMethodList = new List<SelectListItem>
            {
                new SelectListItem { Value = "CreditCard", Text = "Credit Card" },
                new SelectListItem { Value = "BankTransfer", Text = "Bank Transfer" },
                new SelectListItem { Value = "Cash", Text = "Cash" }
            };
        }
    }
}
