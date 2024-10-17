using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace KPCOS.APIService.Controllers
{
    [Route("odata/Invoices")]
    public class InvoiceControllerOData : ODataController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceControllerOData(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        // GET: odata/Invoices
        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var result = await _invoiceService.GetAll();
            return Ok(result);
        }

        // GET: odata/Invoices(5)
        [HttpGet("({id})")]
        public async Task<ActionResult<Invoice>> GetInvoice([FromODataUri] string id)
        {
            var result = await _invoiceService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // PUT: odata/Invoices(5)
        [HttpPut("({id})")]
        public async Task<IActionResult> PutInvoice([FromODataUri] string id, [FromBody] Invoice request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming Save method returns the updated invoice or null if not found
            var updatedInvoice = await _invoiceService.Save(request);
            if (updatedInvoice == null)
            {
                return NotFound();
            }

            return Ok(updatedInvoice);
        }

        // POST: odata/Invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice([FromBody] Invoice request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdInvoice = await _invoiceService.Save(request);
            return Created(createdInvoice);
        }

    }
}
