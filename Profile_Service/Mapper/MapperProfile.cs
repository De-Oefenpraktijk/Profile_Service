using AutoMapper;
using EventBus.Messages.Events;
using Profile_Service.DTO;
using Profile_Service.Entities;

namespace Profile_Service.Mapper

{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<User, ProfileUpdatedEvent>().ReverseMap();
            CreateMap<UserDTO, ProfileUpdatedEvent>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
