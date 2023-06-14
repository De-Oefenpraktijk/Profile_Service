using AutoMapper;
using Profile_Service.DTO;
using Profile_Service.Entities;
namespace Profile_Service.Mapper
    
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, OutputUserDTO>().ReverseMap();
            CreateMap<User, InputUserDTO>().ReverseMap();
            CreateMap<User, InputUpdateUserDTO>().ReverseMap();

            CreateMap<Education, EducationDTO>().ReverseMap();

            CreateMap<Specialization, SpecializationDTO>().ReverseMap();

            CreateMap<Function, AddFunctionDTO>().ReverseMap();
            CreateMap<Function, FunctionDTO>().ReverseMap();
            CreateMap<User, UserActivityStatusDTO>().ReverseMap();
            //CreateMap<SpecializationDTO, Specialization>().ReverseMap();
        }
    }
}