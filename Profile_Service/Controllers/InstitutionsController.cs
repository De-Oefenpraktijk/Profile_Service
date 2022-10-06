using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Profile_Service.DTO;
using Profile_Service.Entities;
using Profile_Service.Models;

namespace Profile_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly DBContext _context;

        public InstitutionsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Institutions
        [HttpGet("GetInstitutions")]
        public async Task<ActionResult<IEnumerable<InstitutionsDTO>>> Get()
        {
            var List = await _context.Institutions.Select(
                s => new InstitutionsDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        // POST: api/Institutions
        [HttpPost("NewInstitution")]
        public async Task<IActionResult> Post(InstitutionsDTO[] institutions)
        {
            foreach (var institution in institutions)
            {
                var entity = new Institutions()
                {
                    Id=institution.Id,
                    Name=institution.Name,
                };

                _context.Institutions.Add(entity);
                await _context.SaveChangesAsync();
            }
            
            return Ok();
        }

        // DELETE: api/Institutions/5
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var institutions = await _context.Institutions.FindAsync(id);
            if (institutions == null)
            {
                return NotFound();
            }

            _context.Institutions.Remove(institutions);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
