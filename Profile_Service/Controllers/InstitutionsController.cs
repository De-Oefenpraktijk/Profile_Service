using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class InstitutionsController : ControllerBase
    {
        private readonly InstitutionService _institutionService;

        public InstitutionsController(InstitutionService institutionService)
        {
            _institutionService = institutionService;
        }

        // GET: api/Institutions
        [HttpGet("GetInstitutions")]
        public async Task<ActionResult<IEnumerable<InstitutionsDTO>>> Get()
        {
            var institutions = await _institutionService.GetInstitutions();
            return Ok(institutions);
        }

        // POST: api/Institutions
        [HttpPost("NewInstitution")]
        public async Task<IActionResult> Post(Institutions institution)
        {
            await _institutionService.AddInstitution(institution);
            return Ok();
        }

        // DELETE: api/Institutions/5
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _institutionService.DeleteInstitution(id);
            return Ok();
        }
    }
}
