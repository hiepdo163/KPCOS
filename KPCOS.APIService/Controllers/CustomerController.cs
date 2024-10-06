using KPCOS.Data.Models;
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
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpGet]
        public async Task<IBusinessResult> GetCustomers()
        {
            return await _customerService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetACustomer([FromRoute] string id)
        {
            return await _customerService.GetById(id);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutACustomer(Customer request)
        {
            return await _customerService.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateCustomer(Customer request)
        {
            return await _customerService.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteCustomer([FromRoute] string id)
        {
            return await _customerService.DeleteById(id);
        }
    }
}
