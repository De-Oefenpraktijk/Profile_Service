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
    public class ThemesController : ControllerBase
    {
        private readonly ThemeService _themeService;

        public ThemesController(ThemeService themeService)
        {
            _themeService = themeService;
        }

        // GET: api/Themes
        [HttpGet("GetThemes")]
        public async Task<ActionResult<IEnumerable<ThemesDTO>>> Get()
        {
            var themes = await _themeService.GetThemes();
            return Ok(themes);
        }

        // POST: api/Themes
        [HttpPost("NewInstitution")]
        public async Task<IActionResult> Post(Themes theme)
        {
            await _themeService.AddTheme(theme);
            return Ok();
        }

        // DELETE: api/Themes/5
        [HttpDelete("DeleteInstitution/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _themeService.DeleteTheme(id);
            return Ok();
        }
    }
}
