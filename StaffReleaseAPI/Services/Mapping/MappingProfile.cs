using AutoMapper;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Models.DTOs.UserDtoFolder;

namespace StaffReleaseAPI.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterRequestDto, ApplicationUser>()
                          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserName))
                          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                          .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                      .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
                      .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
                      .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

        }
    }
