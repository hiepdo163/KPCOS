using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController(IInvoiceService invoiceService) : ControllerBase
    {
        private readonly IInvoiceService _invoiceService = invoiceService;

        [HttpGet]
        public async Task<IBusinessResult> GetInvoices(
            string searchId = null,
            string paymentMethod = null,
            string status = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int pageNumber = 1,
            int pageSize = 3)
        {
            return await _invoiceService.GetAll(searchId, paymentMethod, status, startDate, endDate, pageNumber, pageSize);
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetInvoice([FromRoute] string id)
        {
            return await _invoiceService.GetById(id);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutAInvoice(Invoice request)
        {
            return await _invoiceService.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAInvoice(Invoice request)
        {
            return await _invoiceService.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAInvoice([FromRoute] string id)
        {
            return await _invoiceService.DeleteById(id);
        }
        [HttpGet("search/{searchId}")]
        public async Task<IBusinessResult> SearchInvoice([FromRoute] string searchId)
        {
            return await _invoiceService.SearchById(searchId);
        }
    }
}
