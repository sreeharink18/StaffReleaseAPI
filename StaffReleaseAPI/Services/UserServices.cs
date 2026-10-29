using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Models.DTOs.UserDtoFolder;
using StaffReleaseAPI.Repository.IRepository;
using StaffReleaseAPI.Services.Helper;
namespace StaffReleaseAPI.Services
{
    public class UserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserServices(IUnitOfWork unitOfWork,
             SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ApiResponse> GetUsersDetails(string id)
        {
            ApplicationUser userFromDb = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == id);
            if (userFromDb == null)
            {
                return ApiResponseHelper.ErrorResponse("User not found");
            }
            var roles = await _userManager.GetRolesAsync(userFromDb);


            UserLoginResponseDto response = new()
            {
                Name = userFromDb.Name,
                Email = userFromDb.UserName,
                Id = userFromDb.Id,
                Role = roles.FirstOrDefault()

            };
            if (response.Email == null || string.IsNullOrEmpty(response.Id))
            {
                return ApiResponseHelper.ErrorResponse("UserName or Password is incorrect");
            }
            return ApiResponseHelper.SuccessResponse(response);
        }
    }
}
