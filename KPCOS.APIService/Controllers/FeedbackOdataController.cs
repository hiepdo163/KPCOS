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
    public class FeedbackOdataController : ODataController // Change to ODataController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackOdataController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedback
        [HttpGet]
        [EnableQuery]
        public IQueryable<Feedback> GetFeedbacks()
        {
            // Assuming _feedbackService.GetAll() returns a Task<BusinessResult<List<Feedback>>>
            var businessResult = _feedbackService.GetAll().Result;

            if (businessResult.Data is IEnumerable<Feedback> feedbacks)
            {
                // Convert IEnumerable<Feedback> to IQueryable<Feedback>
                return feedbacks.AsQueryable();
            }

            // If the data isn't available or in an incorrect format, return an empty IQueryable
            return Enumerable.Empty<Feedback>().AsQueryable();
        }

    }
}
