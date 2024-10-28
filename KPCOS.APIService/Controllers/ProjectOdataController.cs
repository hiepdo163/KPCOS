using KPCOS.Service.Interface;
using KPCOS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KPCOS.Service.Base;
using Microsoft.AspNetCore.OData.Query;
using KPCOS.Service.DTOs;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using KPCOS.Data.Base;
using KPCOS.Data.Repository;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectOdataController : ODataController 
    {
        GenericRepository<Project> repository;

        public ProjectOdataController()
        {
            repository = new ProjectRepository();
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var result = await repository.GetAllAsync();
            return Ok(result);
        }
    
    }
}
