using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KPCOS.Data.Models;
using KPCOS.Common;

namespace KPCOS.MVCWebApp.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly FA24_SE1717_PRN231_G4_KPCOSContext _context;
        
        public ProjectsController(FA24_SE1717_PRN231_G4_KPCOSContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var fA24_SE1717_PRN231_G4_KPCOSContext = _context.Projects.Include(p => p.ConstructionStaff).Include(p => p.Customer).Include(p => p.Designer);
            return View(await fA24_SE1717_PRN231_G4_KPCOSContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ConstructionStaff.User)
                .Include(p => p.Customer.User)
                .Include(p => p.Designer.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        // GET: Projects/Create
        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["ConstructionStaffId"] = new SelectList(
                _context.Employees.Include(e => e.User).Select(e => new { e.Id, FullName = e.User.Fullname }),
                "Id",
                "FullName"
            );

            ViewData["DesignerId"] = new SelectList(
                _context.Employees.Include(e => e.User).Select(e => new { e.Id, FullName = e.User.Fullname }),
                "Id",
                "FullName"
            );

            ViewData["CustomerId"] = new SelectList(
                _context.Customers.Include(c => c.User).Select(c => new { c.Id, FullName = c.User.Fullname }),
                "Id",
                "FullName"
            );

            return View();
        }



        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActualCost,ConstructionStaffId,CustomerId,DesignerId,EndDate,EstimatedCost,StartDate,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConstructionStaffId"] = new SelectList(_context.Employees, "Id", "Id", project.ConstructionStaffId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", project.CustomerId);
            ViewData["DesignerId"] = new SelectList(_context.Employees, "Id", "Id", project.DesignerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ConstructionStaffId"] = new SelectList(
                _context.Employees.Include(e => e.User).Select(e => new { e.Id, FullName = e.User.Fullname }),
                "Id",
                "FullName"
            );

            ViewData["DesignerId"] = new SelectList(
                _context.Employees.Include(e => e.User).Select(e => new { e.Id, FullName = e.User.Fullname }),
                "Id",
                "FullName"
            );

            ViewData["CustomerId"] = new SelectList(
                _context.Customers.Include(c => c.User).Select(c => new { c.Id, FullName = c.User.Fullname }),
                "Id",
                "FullName"
            );
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ActualCost,ConstructionStaffId,CustomerId,DesignerId,EndDate,EstimatedCost,StartDate,Status")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["ConstructionStaffId"] = new SelectList(_context.Employees, "Id", "Id", project.ConstructionStaffId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", project.CustomerId);
            ViewData["DesignerId"] = new SelectList(_context.Employees, "Id", "Id", project.DesignerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ConstructionStaff)
                .Include(p => p.Customer)
                .Include(p => p.Designer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var feedbacks = await _context.Feedbacks.Where(f => f.ProjectId == id).ToListAsync();
            if (feedbacks != null)
            {
                _context.Feedbacks.RemoveRange(feedbacks);
                var project = await _context.Projects.FindAsync(id);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                }
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
