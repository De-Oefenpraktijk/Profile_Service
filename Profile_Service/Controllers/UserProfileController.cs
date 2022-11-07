using Microsoft.AspNetCore.Mvc;
using System.Net;
using Profile_Service.Entities;
using Profile_Service.DTO;
using Profile_Service.Services;

namespace Profile_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(string Id)
        {
            var user = await _userService.GetUserByID(Id);
            if (user == null)
                return NotFound();
                
            return Ok(user);
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(User User)
        {
            await _userService.CreateUser(User);
            return HttpStatusCode.OK;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(User User)
        {
            await _userService.UpdateUser(User);
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(string Id)
        {
            await _userService.DeleteUser(Id);
            return HttpStatusCode.OK;
        }
    }
}

