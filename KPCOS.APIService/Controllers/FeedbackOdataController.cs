using KPCOS.Data.Models;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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
        public async Task<IActionResult> GetFeedbacks()
        {
            var result = await _feedbackService.GetAll();
            return Ok(result);
        }

    }
}
