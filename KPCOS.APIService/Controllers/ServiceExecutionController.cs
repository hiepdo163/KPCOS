using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using KPCOS.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceExecutionController(IServiceExecutionService serviceExecutionService) : ControllerBase
    {
        private readonly IServiceExecutionService _serviceExecutionService = serviceExecutionService;

        [HttpGet]
        public async Task<IBusinessResult> GetServiceExecutions([FromQuery]QueryPagedServiceExecution query)
        {
            return await _serviceExecutionService.GetAll(query);
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAServiceExecution([FromRoute] string id)
        {
            return await _serviceExecutionService.GetById(id);
        }
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutAServiceExecution([FromRoute] string id, ServiceExecutionDTO request)
        {
            return await _serviceExecutionService.Update(id, request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAServiceExecution(ServiceExecutionDTO request)
        {
            return await _serviceExecutionService.Create(request);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IBusinessResult> DeleteAServiceExecution([FromRoute] string id)
        {
            return await _serviceExecutionService.DeleteById(id);
        }
    }
}
