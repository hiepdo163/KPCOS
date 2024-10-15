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
    public class ServiceAssignmentController(IServiceAssignmentService serviceAssignmentService) : ControllerBase
    {
        private readonly IServiceAssignmentService _serviceAssignmentService = serviceAssignmentService;

        [HttpGet]
        public async Task<IBusinessResult> GetServiceAssignments()
        {
            return await _serviceAssignmentService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAServiceAssignment([FromRoute] string id)
        {
            return await _serviceAssignmentService.GetById(id);
        }
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutAServiceAssignment([FromRoute] string id, ServiceAssignmentDTO request)
        {
            return await _serviceAssignmentService.Update(id, request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAServiceAssignment(ServiceAssignmentDTO request)
        {
            return await _serviceAssignmentService.Create(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAServiceAssignment([FromRoute] string id)
        {
            return await _serviceAssignmentService.DeleteById(id);
        }
    }
}
