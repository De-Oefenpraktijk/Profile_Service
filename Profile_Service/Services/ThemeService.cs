using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class ThemeService
    {
        private readonly DBContext _context;
        
        public ThemeService(DBContext context)
        {
            _context = context;
        }

        public async Task<ThemesDTO> AddTheme(Themes theme)
        {
            await _context.Themes.InsertOneAsync(theme);
            return new ThemesDTO(theme);
        }

        public async Task<string> DeleteTheme(string themeId)
        {
            await _context.Themes.DeleteOneAsync(x => x.Id == themeId);
            return themeId;
        }

        public async Task<ThemesDTO> GetThemeByID(string themeId)
        {
            var theme = await _context.Themes.Find(x => x.Id == themeId).FirstOrDefaultAsync();
            if (theme == null)
            {
                throw new Exception("Theme does not exist");
            }
            return new ThemesDTO(theme);
        }

        public async Task<IEnumerable<ThemesDTO>> GetThemes()
        {
            var result = await _context.Themes.Find(_ => true).ToListAsync();
            return result.Select(x => new ThemesDTO(x)).ToList();
        }

        public async Task<ThemesDTO> UpdateTheme(Themes theme)
        {
            await _context.Themes.ReplaceOneAsync(x => x.Id == theme.Id, theme);
            return new ThemesDTO(theme);
        }
    }
}
