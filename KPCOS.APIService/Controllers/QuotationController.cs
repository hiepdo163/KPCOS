using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private readonly IQuotationService _service;

        public QuotationController(IQuotationService service)
        {
            _service = service;
        }

        [HttpGet("design/{designId}")]
        public async Task<IBusinessResult> GetQuotationByDesign([FromRoute]string designId)
        {
            return await _service.GetByDesignId(designId);
        }

        [HttpGet]
        public async Task<IBusinessResult> GetQuotation()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetQuotationById([FromRoute]string id)
        {
            return await _service.GetById(id);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> RemoveQuotation([FromRoute] string id)
        {
            return await _service.DeleteById(id);
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateQuotation([FromBody] Quotation quotation)
        {
            return await _service.Save(quotation);
        }

        [HttpPost]
        public async Task<IBusinessResult> CreateQuotation([FromBody] Quotation quotation)
        {
            return await _service.Save(quotation);
        }
    }
}
