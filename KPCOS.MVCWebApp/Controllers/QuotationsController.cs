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

    }
}
