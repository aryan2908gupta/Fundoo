using AutoMapper;
using ModelLayer.Entities;
using ModelLayer.Dto;

namespace BusinessLogicLayer.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>()
                .ReverseMap();

            CreateMap<UserSignUpRequestDto, User>();
        }
    }
}
