using AutoMapper;
using EventBus.Messages.Events;
using Profile_Service.DTO;
using Profile_Service.Entities;
namespace Profile_Service.Mapper
    
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, ProfileUpdatedEvent>().ReverseMap();
            CreateMap<OutputUserDTO, ProfileUpdatedEvent>().ReverseMap();
            CreateMap<User, OutputUserDTO>().ReverseMap();
            CreateMap<User, InputUserDTO>().ReverseMap();

            CreateMap<Education, EducationDTO>().ReverseMap();

            CreateMap<Specialization, SpecializationDTO>().ReverseMap();

            CreateMap<Function, AddFunctionDTO>().ReverseMap();
            CreateMap<Function, FunctionDTO>().ReverseMap();
            //CreateMap<SpecializationDTO, Specialization>().ReverseMap();
        }
    }
}