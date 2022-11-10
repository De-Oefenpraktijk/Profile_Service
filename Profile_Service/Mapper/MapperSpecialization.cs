using Profile_Service.DTO;
using Profile_Service.Entities;
using AutoMapper;
using MongoDB.Driver;

namespace Profile_Service.Mapper
{
    public class MapperSpecialization : Profile
    {
        public MapperSpecialization()
        {
            CreateMap<Specialization, SpecializationDTO>().ReverseMap();
            CreateMap<SpecializationDTO, Specialization>().ReverseMap();
        }

    }
}
