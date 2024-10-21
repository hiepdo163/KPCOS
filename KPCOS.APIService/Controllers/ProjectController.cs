using KPCOS.Service.Interface;
using KPCOS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetProjects()
        {
            return await _projectService.GetAll();
   
        }
        [HttpGet("search")]
        public async Task<IBusinessResult> GetProjects([FromQuery] string? customerName, [FromQuery] string? designerId, [FromQuery] DateTime? startDate, [FromQuery] string? status)
        {
            return await _projectService.GetProjectsAsync(customerName, designerId, startDate, status);
        }



        [HttpGet("{projectId}")]
        public async Task<IBusinessResult> GetProjectById(string projectId)
        {
            return await _projectService.GetById(projectId);
        }

        [HttpPost]
        public async Task<IBusinessResult> CreateProject(Project project)
        {
            return await _projectService.Save(project);

        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateProject(Project project)
        {
            return await _projectService.Save(project);

        }

        [HttpDelete("{projectId}")]
        public async Task<IBusinessResult> DeleteProject(string projectId)
        {
            return await _projectService.DeleteById(projectId);
        }
    }
}
