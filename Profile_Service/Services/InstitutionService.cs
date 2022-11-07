using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class InstitutionService
    {
        private readonly DBContext _context;
        
        public InstitutionService(DBContext context)
        {
            _context = context;
        }

        public async Task<InstitutionsDTO> AddInstitution(Institutions institution)
        {
            await _context.Institutions.InsertOneAsync(institution);
            return new InstitutionsDTO(institution);
        }

        public async Task<string> DeleteInstitution(string institutionId)
        {
            await _context.Institutions.DeleteOneAsync(x => x.Id == institutionId);
            return institutionId;
        }

        public async Task<InstitutionsDTO> GetInstitutionByID(string institutionId)
        {
            var institution = await _context.Institutions.Find(x => x.Id == institutionId).FirstOrDefaultAsync();
            if (institution == null)
            {
                throw new Exception("Institutoin does not exist");
            }
            return new InstitutionsDTO(institution);
        }

        public async Task<IEnumerable<InstitutionsDTO>> GetInstitutions()
        {
            var result = await _context.Institutions.Find(_ => true).ToListAsync();
            return result.Select(x => new InstitutionsDTO(x)).ToList();
        }

        public async Task<InstitutionsDTO> UpdateInstitution(Institutions institution)
        {
            await _context.Institutions.ReplaceOneAsync(x => x.Id == institution.Id, institution);
            return new InstitutionsDTO(institution);
        }
    }
}
