using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }


        // GET: api/Feedbacks
        [HttpGet]
        
        public async Task<IBusinessResult> GetFeedbacks()
        {
            return await _feedbackService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetFeedbacksById(string id)
        {
            return await _feedbackService.GetById(id);
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateFeedbacks(FeedbackDTO feedback)
        {
            return await _feedbackService.Update(feedback); 
        }

        [HttpPost]
        public async Task<IBusinessResult> CreateFeedback(FeedbackDTO feedback)
        {
            return await _feedbackService.Create(feedback);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteFeedback(string id)
        {
            return await _feedbackService.DeleteById(id);
        }
    }
}
