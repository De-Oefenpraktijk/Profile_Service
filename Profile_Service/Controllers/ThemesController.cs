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
    public class ThemesController : ControllerBase
    {
        private readonly DBContext _context;

        public ThemesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Themes
        [HttpGet("GetThemes")]
        public async Task<ActionResult<IEnumerable<ThemesDTO>>> Get()
        {
            var List = await _context.Themes.Select(
                s => new ThemesDTO
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

        // POST: api/Themes
        [HttpPost("NewInstitution")]
        public async Task<IActionResult> Post(ThemesDTO[] Themes)
        {
            foreach (var institution in Themes)
            {
                var entity = new Themes()
                {
                    Id=institution.Id,
                    Name=institution.Name,
                };

                _context.Themes.Add(entity);
                await _context.SaveChangesAsync();
            }
            
            return Ok();
        }

        // DELETE: api/Themes/5
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var Themes = await _context.Themes.FindAsync(id);
            if (Themes == null)
            {
                return NotFound();
            }

            _context.Themes.Remove(Themes);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
