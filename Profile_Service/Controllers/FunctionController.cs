﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [Authorize()]
        public async Task<ActionResult<IEnumerable<FunctionDTO>>> GetAll()
        {
            var functions = await _functionService.GetAll();
            return Ok(functions);
        }

        [HttpPost("AddFunction")]
        [Authorize("manage:functions")]
        public async Task<IActionResult> AddFunction(AddFunctionDTO function)
        {
            FunctionDTO newFunction = await _functionService.AddFunction(function);
            return Ok(newFunction);
        }

        [HttpDelete("DeleteFunction")]
        [Authorize("manage:functions")]
        public async Task<IActionResult> DeleteFunction(string id)
        {
            var result = await _functionService.DeleteFunction(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
