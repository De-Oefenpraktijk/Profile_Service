using Microsoft.AspNetCore.Mvc;
using System.Net;
using Profile_Service.Entities;
using Profile_Service.DTO;
using Profile_Service.Services;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MassTransit;
using AutoMapper;
using EventBus.Messages.Events;
using MongoDB.Bson;

namespace Profile_Service.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publish;



        public UserController(UserService userService, IMapper mapper, IPublishEndpoint publish)
        {
            _userService = userService;
            _publish = publish;
            _mapper = mapper;
        }



        [HttpGet("GetUserById/{Id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(string Id)
        {
            var user = await _userService.GetUserByID(Id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }



        [HttpPost("InsertUser")]
        public async Task<ActionResult> InsertUser(UserDTO User)
        {
            var result = await _userService.CreateUser(User);

            // If result, map to event "model" and publish to MQ.
            if (result != null)
            {
                var message = _mapper.Map<ProfileUpdatedEvent>(result);
                await _publish.Publish(message);
            }

            return Ok(User);
        }



        [HttpPut("UpdateUser/{Id}")]
        public async Task<ActionResult> UpdateUser(UserDTO User, string Id)
        {
            var updatedUser = await _userService.UpdateUser(User, Id);

            if (updatedUser != null)
            {
                var message = _mapper.Map<ProfileUpdatedEvent>(updatedUser);
                await _publish.Publish(message);
            }

            return Ok(updatedUser);
        }



        [HttpDelete("DeleteUser/{Id}")]
        public async Task<ActionResult> DeleteUser(string Id)
        {
            await _userService.DeleteUser(Id);
            return Ok();
        }
    }
}