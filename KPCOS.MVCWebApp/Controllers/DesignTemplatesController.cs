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
    public class DesignTemplatesController : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;

        public DesignTemplatesController(FA24_SE1717_PRN231_G4_KPCOSContext context)
        {
            _context = context;
        }

        // GET: DesignTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.DesignTemplates.ToListAsync());
        }

        // GET: DesignTemplates/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designTemplate = await _context.DesignTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designTemplate == null)
            {
                return NotFound();
            }

            return View(designTemplate);
        }

        // GET: DesignTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DesignTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DefaultLocation,DefaultShape,DefaultSize,Description,Image,Name,TotalPrice")] DesignTemplate designTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designTemplate);
        }

        // GET: DesignTemplates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designTemplate = await _context.DesignTemplates.FindAsync(id);
            if (designTemplate == null)
            {
                return NotFound();
            }
            return View(designTemplate);
        }

        // POST: DesignTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,DefaultLocation,DefaultShape,DefaultSize,Description,Image,Name,TotalPrice")] DesignTemplate designTemplate)
        {
            if (id != designTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignTemplateExists(designTemplate.Id))
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
            return View(designTemplate);
        }

        // GET: DesignTemplates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designTemplate = await _context.DesignTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designTemplate == null)
            {
                return NotFound();
            }

            return View(designTemplate);
        }

        // POST: DesignTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var designTemplate = await _context.DesignTemplates.FindAsync(id);
            if (designTemplate != null)
            {
                _context.DesignTemplates.Remove(designTemplate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignTemplateExists(string id)
        {
            return _context.DesignTemplates.Any(e => e.Id == id);
        }
    }
}
