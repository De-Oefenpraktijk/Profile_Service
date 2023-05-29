using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Profile_Service.DTO;
using Profile_Service.Entities;
using Profile_Service.Services;

namespace Profile_Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _specializationService;

        public SpecializationController(SpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        // GET: api/Themes
        [HttpGet("GetAllSpecializations")]
        [Authorize()]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAll()
        {
            var themes = await _specializationService.GetSpecialization();
            return Ok(themes);
        }

        // GET: api/Themes/5
        [HttpGet("GetSpecialization/{Id}")]
        [Authorize()]
        public async Task<ActionResult<IEnumerable<EducationDTO>>> GetById(string Id)
        {
            var specialization = await _specializationService.GetSpecializationByID(Id);
            if (specialization == null)
                return NotFound();

            return Ok(specialization);
        }

        // POST: api/Themes
        [HttpPost("PostSpecialization")]
        [Authorize("manage:specializations")]
        public async Task<IActionResult> Post(SpecializationDTO specialization)
        {
            SpecializationDTO result = await _specializationService.AddSpecialization(specialization);
            return Ok(result);
        }

        // DELETE: api/Themes/5
        [HttpDelete("DeleteSpecialization/{id}")]
        [Authorize("manage:specializations")]
        public async Task<IActionResult> Delete(string id)
        {
            await _specializationService.DeleteSpecialization(id);
            return Ok();
        }
    }
}
