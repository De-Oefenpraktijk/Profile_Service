using EventBus.Messages.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Profile_Service.DTO;
using Profile_Service.Entities;
using Profile_Service.Services;

namespace Profile_Service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly EducationService _educationService;

        public EducationController(EducationService educationService)
        {
            _educationService = educationService;
        }

        // GET: api/Education
        [HttpGet("GetEducations")]
        [Authorize()]
        public async Task<ActionResult<IEnumerable<EducationDTO>>> Get()
        {
            var educations = await _educationService.GetEducations();
            return Ok(educations);
        }

        // GET: api/Education/5
        [HttpGet("GetEducation/{Id}")]
        [Authorize()]
        public async Task<ActionResult<IEnumerable<EducationDTO>>> GetById(string Id)
        {
            var education = await _educationService.GetEducationByID(Id);
            if (education == null)
                return NotFound();

            return Ok(education);
        }

        // POST: api/Education
        [HttpPost("NewEducation")]
        [Authorize("manage:educations")]
        public async Task<IActionResult> Post(EducationDTO education)
        {
            await _educationService.AddEducation(education);
            return Ok();
        }

        // PUT: api/Education/5
        [HttpPut("UpdateEducation/{Id}")]
        [Authorize("manage:educations")]
        public async Task<ActionResult> UpdateEducation(EducationDTO education, string Id)
        {
            var updatedEducation = await _educationService.UpdateEducation(education, Id);

            return Ok(updatedEducation);
        }

        // DELETE: api/Education/5
        [HttpDelete("DeleteEducation/{Id}")]
        [Authorize("manage:educations")]
        public async Task<IActionResult> Delete(string Id)
        {
            await _educationService.DeleteEducation(Id);
            return Ok();
        }
    }
}
