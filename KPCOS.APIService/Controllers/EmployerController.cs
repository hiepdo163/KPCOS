using KPCOS.Data.Models;
using KPCOS.Service.Base;
using KPCOS.Service.DTOs;
using KPCOS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KPCOS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetEmployees()
        {
            return await _employeeService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetEmployeeById(string id)
        {
            return await _employeeService.GetById(id);
        }

        [HttpPost]
        public async Task<IBusinessResult> CreateEmployee([FromBody] Employee employee)
        {
            return await _employeeService.Save(employee);
        }

        [HttpPut]
        public async Task<IBusinessResult> UpdateEmployee([FromBody] Employee employee)
        {
            return await _employeeService.Save(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteEmployee(string id)
        {
            return await _employeeService.DeleteById(id);
        }
    }
}
