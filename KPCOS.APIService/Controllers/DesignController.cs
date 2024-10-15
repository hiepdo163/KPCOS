using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignController(IDesignService designService) : ControllerBase
    {
        private readonly IDesignService _designService = designService;

        [HttpGet]
        public async Task<IBusinessResult> GetDesigns()
        {
            return await _designService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetADesign([FromRoute] string id)
        {
            return await _designService.GetById(id);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutADesign(Design request)
        {
            return await _designService.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateDesign(Design request)
        {
            return await _designService.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteDesign([FromRoute] string id)
        {
            return await _designService.DeleteById(id);
        }
    }
}
