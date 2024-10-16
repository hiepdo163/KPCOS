using KPCOS.Data.Models;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
namespace KPCOS.APIService.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class FeedbackOdataController : ODataController 
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackOdataController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedback
        [HttpGet]
        [EnableQuery]
        public async Task<IQueryable<Feedback>> GetFeedbacks()
        {
            
            var businessResult = await _feedbackService.GetAll();

            if (businessResult.Data is IEnumerable<Feedback> feedbacks)
            {
                return feedbacks.AsQueryable();
            }

            //neu sai format, return thanh enum
            return Enumerable.Empty<Feedback>().AsQueryable();
        }

    }
}
