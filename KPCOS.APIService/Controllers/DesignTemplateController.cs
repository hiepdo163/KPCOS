using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using KPCOS.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignTemplateController(IDesignTemplateService designTemplateService) : ControllerBase
    {
        private readonly IDesignTemplateService _designTemplateService1 = designTemplateService;

        [HttpGet]
        public async Task<IBusinessResult> GetDesignTemplates()
        {
            return await _designTemplateService1.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetADesignTemplate([FromRoute] string id)
        {
            return await _designTemplateService1.GetById(id);
        }
        [HttpGet("filter/{name}")]
        public async Task<IBusinessResult> GetByName([FromRoute] string name)
        {
            return await _designTemplateService1.GetWithCondition(name);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutADesignTemplate(DesignTemplate request)
        {
            return await _designTemplateService1.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateDesignTemplate(DesignTemplate request)
        {
            return await _designTemplateService1.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteDesignTemplate([FromRoute] string id)
        {
            return await _designTemplateService1.DeleteById(id);
        }
    }
}
