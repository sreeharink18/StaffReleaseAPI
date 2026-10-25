using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Models.DTOs.UserDtoFolder;
using StaffReleaseAPI.Repository.IRepository;
using StaffReleaseAPI.Services.Helper;
using System.Net;

namespace StaffReleaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork unitOfWork,
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
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto model)
        {

            ApplicationUser userFromDb = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.UserName == model.UserName);
            if (userFromDb == null)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("Email is not Exist Plz check once more"));
            }
            if (userFromDb.LockoutEnd.HasValue && userFromDb.LockoutEnd > DateTimeOffset.UtcNow)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("User is locked out. Please try again later.", HttpStatusCode.Forbidden));
            }

            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);
            if (isValid == false)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("UserName or Password is incorrect"));
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
                return BadRequest(ApiResponseHelper.ErrorResponse("UserName or Password is incorrect"));
            }
            return Ok(ApiResponseHelper.SuccessResponse(response));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto model)
        {
            ApplicationUser UserFromDb = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (UserFromDb != null)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("User is already exist"));
            }
            var newUserDto = _mapper.Map<ApplicationUser>(model);

            try
            {
                var result = await _userManager.CreateAsync(newUserDto, model.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(StaticData.Role_Admin).GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticData.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(StaticData.Role_User));
                        await _roleManager.CreateAsync(new IdentityRole(StaticData.Role_HRStaff));
                        await _roleManager.CreateAsync(new IdentityRole(StaticData.Role_HRManager));
                        await _roleManager.CreateAsync(new IdentityRole(StaticData.Role_DepartmentManager));
                    }
                    if (model.Role == StaticData.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(newUserDto, StaticData.Role_Admin);
                    }
                    else if (model.Role == StaticData.Role_HRStaff)
                    {
                        await _userManager.AddToRoleAsync(newUserDto, StaticData.Role_HRStaff);
                    }
                    else if (model.Role == StaticData.Role_HRManager)
                    {
                        await _userManager.AddToRoleAsync(newUserDto, StaticData.Role_HRManager);
                    }
                    else if (model.Role == StaticData.Role_DepartmentManager)
                    {
                        await _userManager.AddToRoleAsync(newUserDto, StaticData.Role_DepartmentManager);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUserDto, StaticData.Role_User);
                    }
                    return Ok(ApiResponseHelper.SuccessResponse("User is Register Successfully! Plz countinue..."));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse(ex.Message));
            }
            return BadRequest(ApiResponseHelper.ErrorResponse("Internal Server Issue...", HttpStatusCode.InternalServerError));
        }
    }
}
