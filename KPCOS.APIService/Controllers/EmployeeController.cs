﻿using KPCOS.Data.Models;
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
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet]
        public async Task<IBusinessResult> GetEmployees()
        {
            return await _employeeService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAnEmployee([FromRoute] string id)
        {
            return await _employeeService.GetById(id);
        }
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutAnEmployee([FromRoute] string id, EmployeeDTO request)
        {
            return await _employeeService.Update(id, request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAnEmployee(EmployeeDTO request)
        {
            return await _employeeService.Create(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAnEmployee([FromRoute] string id)
        {
            return await _employeeService.DeleteById(id);
        }
    }
}