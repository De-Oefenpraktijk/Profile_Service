﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using Profile_Service.Entities;
using Profile_Service.DTO;
using Profile_Service.Services;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using AutoMapper;
using EventBus.Messages.Events;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace Profile_Service.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;



        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }



        [HttpGet("GetUserById/{Id}")]
        public async Task<ActionResult<OutputUserDTO>> GetUserById(string Id)
        {
            OutputUserDTO user = await _userService.GetUserByID(Id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }



        [HttpPost("InsertUser")]
        public async Task<ActionResult> InsertUser(InputUserDTO User)
        {
            OutputUserDTO newUser = await _userService.CreateUser(User);
            return Ok(newUser);
        }



        [HttpPut("UpdateUser/{Id}")]
        public async Task<ActionResult> UpdateUser(InputUserDTO User, string Id)
        {
            OutputUserDTO updatedUser = await _userService.UpdateUser(User, Id);
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