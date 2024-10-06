using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using KPCOS.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController(IConsultationService consultationService) : ControllerBase
    {
        private readonly IConsultationService _consultationService = consultationService;

        [HttpGet]
        public async Task<IBusinessResult> GetConsultations()
        {
            return await _consultationService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAConsultation([FromRoute]string id)
        {
            return await _consultationService.GetById(id);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutAConsultation(Consultation request)
        {
            return await _consultationService.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAConsultation(Consultation request)
        {
            return await _consultationService.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAConsultation([FromRoute] string id)
        {
            return await _consultationService.DeleteById(id);
        }
    }
}
