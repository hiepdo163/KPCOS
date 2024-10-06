using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.Interface;
using KPCOS.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController(IPackageService packageService) : ControllerBase
    {
        private readonly IPackageService _packageService = packageService;

        [HttpGet]
        public async Task<IBusinessResult> GetPackages()
        {
            return await _packageService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAPackage([FromRoute] string id)
        {
            return await _packageService.GetById(id);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutAPackage(Package request)
        {
            return await _packageService.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAPackage(Package request)
        {
            return await _packageService.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAPackage([FromRoute] string id)
        {
            return await _packageService.DeleteById(id);
        }
    }
}
