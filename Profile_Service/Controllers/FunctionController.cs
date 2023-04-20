﻿using Microsoft.AspNetCore.Mvc;
using Profile_Service.DTO;
using Profile_Service.Services;

namespace Profile_Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly FunctionService _functionService;

        public FunctionController(FunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet("GetAllFunctions")]
        public async Task<ActionResult<IEnumerable<FunctionDTO>>> GetAll()
        {
            var functions = await _functionService.GetAll();
            return Ok(functions);
        }

        [HttpPost("AddFunction")]
        public async Task<IActionResult> AddFunction(AddFunctionDTO function)
        {
            await _functionService.AddFunction(function);
            return Ok();
        }
    }
}
