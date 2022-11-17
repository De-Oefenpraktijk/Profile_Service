using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class EducationService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public EducationService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EducationDTO> AddEducation(EducationDTO _education)
        {
            var education = _mapper.Map<Education>(_education);
            
            await _context.Education.InsertOneAsync(education);
            
            return _education;
        }

        public async Task<string> DeleteEducation(string educationId)
        {
            await _context.Education.DeleteOneAsync(x => x.Id == educationId);
            
            return educationId;
        }

        public async Task<EducationDTO> GetEducationByID(string educationId)
        {
            var education = await _context.Education.Find(x => x.Id == educationId).FirstOrDefaultAsync();
            if (education == null)
            {
                throw new Exception("Education does not exist");
            }

            return _mapper.Map<EducationDTO>(education);
        }

        public async Task<IEnumerable<EducationDTO>> GetEducations()
        {
            IEnumerable<Education> educations = await _context.Education.Find(_ => true).ToListAsync();
            List<EducationDTO> educationDTOs = new List<EducationDTO>();

            foreach (var education in educations)
            {
                var educationDTO = _mapper.Map<EducationDTO>(education);
                
                educationDTOs.Add(educationDTO);
            }
            
            return educationDTOs;
        }

        public async Task<EducationDTO> UpdateEducation(EducationDTO _education, string Id)
        {
            var education = _mapper.Map<Education>(_education);
            education.Id = Id;

            await _context.Education.ReplaceOneAsync(x => x.Id == Id, education);

            return _education;
        }
    }
}
