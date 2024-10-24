using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceFeedbackController : ControllerBase
    {
        private readonly IServiceFeedbackService _serviceFeedbackService;

        public ServiceFeedbackController(IServiceFeedbackService serviceFeedbackService)
        {
            _serviceFeedbackService = serviceFeedbackService;
        }


        // GET: api/Feedbacks
        [HttpGet]

        public async Task<IBusinessResult> GetServiceFeedbacks([FromQuery] ServiceFeedbackFilterParam request)
        {
            return await _serviceFeedbackService.GetAll(request);
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetServiceFeedbackId(string id)
        {
            return await _serviceFeedbackService.GetById(id);
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateServiceFeedbacks(ServiceFeedbackDTO servicefeedback)
        {
            return await _serviceFeedbackService.Update(servicefeedback);
        }

        [HttpPost]
        public async Task<IBusinessResult> CreateServiceFeedbacks(ServiceFeedbackDTO servicefeedback)
        {
            return await _serviceFeedbackService.Create(servicefeedback);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteServiceFeedbacks(string id)
        {
            return await _serviceFeedbackService.DeleteById(id);
        }
    }
}
