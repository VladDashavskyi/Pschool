using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pschool.API.Dto;
using Pschool.API.Enum;
using Pschool.API.Interfaces;
using Pschool.API.Services;
using PSchool.API.DAL.Contexts;
using PSchool.API.DAL.Entities;

namespace Pschool.API.Controllers
{
    [Route("controller")]
    [ApiController]
    public class MainController : ControllerBase
    {

        TenantContext db;
        private readonly ILogger<MainController> _logger;
        private readonly IMainService _mainService;

        public MainController(TenantContext context, ILogger<MainController> logger, IMainService mainServices)
        {
            _logger = logger;
            db = context;
            _mainService = mainServices;
        }

        [HttpGet]
        [Route("/Users")]
        public async Task<IEnumerable<UserDto>> Get(Role? role)
        {
            return await _mainService.GetUsersAsync(role);
            
        }
        
        [HttpGet]
        [Route("/Users{id}")]
        public async Task<UserDto> GetUserById(int id)
        {
            return await _mainService.GetUserByIdAsync(id);
          
        }

        [HttpPost]
        [Route("/User")]
        public async Task<UserDto> CreateUser(UserUpdateDto user)
        {
            return await _mainService.CreateUserAsync(user);

        }

        [HttpPut]
        [Route("/User{id}")]
        public async Task<UserDto> UpdateUser(int id ,UserUpdateDto userDto)
        {
            return await _mainService.UpdateUserAsync(id, userDto);

        }

        [HttpDelete]
        [Route("/User{id}")]
        public async Task DeleteUser(int id)
        {
            await _mainService.DeleteUserAsync(id);
        }

    }
}
