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
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IBusinessResult> GetUsers()
        {
            return await _userService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetAUser([FromRoute] string id)
        {
            return await _userService.GetById(id);
        }
        [HttpPut]
        public async Task<IBusinessResult> PutAUser(User request)
        {
            return await _userService.Save(request);
        }
        [HttpPost]
        public async Task<IBusinessResult> CreateAUser(User request)
        {
            return await _userService.Save(request);
        }
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteAUser([FromRoute] string id)
        {
            return await _userService.DeleteById(id);
        }

        [HttpPost("login")]
        public async Task<IBusinessResult> Login([FromBody] LoginDTO loginDTO)
        {
            return await _userService.LoginAsync(loginDTO);
        }

    }
}
