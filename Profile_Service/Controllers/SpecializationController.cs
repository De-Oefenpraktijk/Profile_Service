using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Profile_Service.DTO;
using Profile_Service.Entities;
using Profile_Service.Services;

namespace Profile_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _specializationService;

        public SpecializationController(SpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        // GET: api/Themes
        [HttpGet("GetSpecialization")]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> Get()
        {
            var themes = await _specializationService.GetSpecialization();
            return Ok(themes);
        }

        // POST: api/Themes
        [HttpPost("NewInstitution")]
        public async Task<IActionResult> Post(SpecializationDTO theme)
        {
            await _specializationService.AddSpecialization(theme);
            return Ok();
        }

        // DELETE: api/Themes/5
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _specializationService.DeleteSpecialization(id);
            return Ok();
        }
    }
}
