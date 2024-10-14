using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;

namespace KPCOS.MVCWebApp.Controllers
{
    public class QuotationsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;

        public QuotationsController(FA24_SE1717_PRN231_G4_KPCOSContext context)
        {
            _context = context;
        }

        // GET: Quotations
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Quotations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .Include(q => q.Design)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // GET: Quotations/Create
        public IActionResult Create()
        {
            ViewData["DesignId"] = new SelectList(_context.Designs, "Id", "Id");
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComplexityLevel,ConsultationAmount,DesignId,Note,QuotationAmount,QuotationDate,Scale,Status,Style")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignId"] = new SelectList(_context.Designs, "Id", "Id", quotation.DesignId);
            return View(quotation);
        }

        // GET: Quotations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return NotFound();
            }
            ViewData["DesignId"] = new SelectList(_context.Designs, "Id", "Id", quotation.DesignId);
            return View(quotation);
        }

        // POST: Quotations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ComplexityLevel,ConsultationAmount,DesignId,Note,QuotationAmount,QuotationDate,Scale,Status,Style")] Quotation quotation)
        {
            if (id != quotation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationExists(quotation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignId"] = new SelectList(_context.Designs, "Id", "Id", quotation.DesignId);
            return View(quotation);
        }

        // GET: Quotations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .Include(q => q.Design)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation != null)
            {
                _context.Quotations.Remove(quotation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotationExists(string id)
        {
            return _context.Quotations.Any(e => e.Id == id);
        }
    }
}
