using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Services
{
    public class SpecializationService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public SpecializationService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Specialization> AddSpecialization(SpecializationDTO _specialization)
        {
            var specialization = _mapper.Map<Specialization>(_specialization);

            await _context.Specialization.InsertOneAsync(specialization);
            return specialization;
        }

        public async Task<ObjectId> DeleteSpecialization(ObjectId specializationId)
        {
            await _context.Specialization.DeleteOneAsync(x => x.Id == specializationId);
            return specializationId;
        }

        public async Task<IEnumerable<SpecializationDTO>> GetSpecialization()
        {

            var result = await _context.Specialization.Find(_ => true).ToListAsync();
            var list = new List<SpecializationDTO>();
            foreach(var i in result)
            {
                var specialization = _mapper.Map<SpecializationDTO>(i);
                list.Add(specialization);
            }

            return list;
        }

        public async Task<Specialization> UpdateSpecialization(SpecializationDTO _specialization, ObjectId _specializationId)
        {
            var specialization = _mapper.Map<Specialization>(_specialization);
            await _context.Specialization.ReplaceOneAsync(x => x.Id == _specializationId, specialization);
            return specialization;
        }
    }
}
