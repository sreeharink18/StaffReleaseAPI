using AutoMapper;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder;
using StaffReleaseAPI.Models.DTOs.StaffReleaseDtoFolder;
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



            CreateMap<RequestDepartmentCreationDto, Department>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<RequestDepartmentUpdationDto, Department>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));


            CreateMap<RequestDepartmentManagerAssignmentDto, DepartmentManagerAssignment>()
                .ForMember(dest => dest.AssignedById, opt => opt.MapFrom(src => src.AssignedById))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.ManagerUserId, opt => opt.MapFrom(src => src.ManagerUserId))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.AssignedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<RequestDepartmentUserAssignmentCreationDto, DepartmentUserAssignment>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.IsManager, opt => opt.MapFrom(src => src.IsManager))
                .ForMember(dest => dest.CanViewClearance, opt => opt.MapFrom(src => src.CanViewClearance))
                .ForMember(dest => dest.CanApproveClearance, opt => opt.MapFrom(src => src.CanApproveClearance))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.AssignedAt, opt => opt.MapFrom(src => DateTime.UtcNow));




            CreateMap<ApplicationUser, ManagerUserDetailDto>();
            CreateMap<DepartmentManagerAssignment, ResponseDepartmentManagerAssignmentDto>();



            //mapping for user details based on the department user assignment
            CreateMap<ApplicationUser, UserDetailDto>();
            CreateMap<DepartmentUserAssignment, ResponseDepartmentUserAssignmentDto>();


            CreateMap<CreateStaffReleaseRequestDto, StaffReleaseRequest>()
               .ForMember(dest => dest.RequestId, opt => opt.Ignore())
               .ForMember(dest => dest.Status, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
               .ForMember(dest => dest.RequiredDepartments, opt => opt.Ignore());

            CreateMap<RequestRequiredDepartment, CreatedClearanceDto>();

            CreateMap<StaffReleaseRequest, StaffReleaseCreatedResponseDto>();

        }
    }
}
