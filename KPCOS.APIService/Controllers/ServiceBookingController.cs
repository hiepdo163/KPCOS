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
    public class ServiceBookingController (IServiceBookingService serviceBookingService) : ControllerBase
    {
        private readonly IServiceBookingService _serviceBookingService = serviceBookingService;

        [HttpGet]
        public async Task<IBusinessResult> GetServiceBookings()
        {
            return await _serviceBookingService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAServiceBooking([FromRoute] string id)
        {
            return await _serviceBookingService.GetById(id);
        }
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutAServiceBooking ([FromRoute] string id, ServiceBookingDTO request)
        {
            return await _serviceBookingService.Update(id, request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAServiceBooking(ServiceBookingDTO request)
        {
            return await _serviceBookingService.Create(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAServiceBooking([FromRoute] string id)
        {
            return await _serviceBookingService.DeleteById(id);
        }
    }
}
