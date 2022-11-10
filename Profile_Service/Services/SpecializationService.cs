using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class SpecializationService
    {
        private readonly DBContext _context;
        
        public SpecializationService(DBContext context)
        {
            _context = context;
        }

        public async Task<SpecializationDTO> AddSpecialization(Specialization theme)
        {
            await _context.Themes.InsertOneAsync(theme);
            return new SpecializationDTO(theme);
        }

        public async Task<string> DeleteSpecialization(string themeId)
        {
            await _context.Themes.DeleteOneAsync(x => x.Id == themeId);
            return themeId;
        }

        public async Task<SpecializationDTO> GetThemeByID(string themeId)
        {
            var theme = await _context.Themes.Find(x => x.Id == themeId).FirstOrDefaultAsync();
            if (theme == null)
            {
                throw new Exception("Theme does not exist");
            }
            return new SpecializationDTO(theme);
        }

        public async Task<IEnumerable<SpecializationDTO>> GetSpecialization()
        {
            var result = await _context.Themes.Find(_ => true).ToListAsync();
            return result.Select(x => new SpecializationDTO(x)).ToList();
        }

        public async Task<SpecializationDTO> UpdateSpecialization(Specialization theme)
        {
            await _context.Themes.ReplaceOneAsync(x => x.Id == theme.Id, theme);
            return new SpecializationDTO(theme);
        }
    }
}
