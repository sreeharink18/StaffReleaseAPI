using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Models.DTOs.DepartmentDtoFolder;
using StaffReleaseAPI.Models.DTOs.UserDtoFolder;
using StaffReleaseAPI.Repository.IRepository;
using StaffReleaseAPI.Services.Helper;
using System.Net;

namespace StaffReleaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
                return Ok(ApiResponseHelper.SuccessResponse(departments));

            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                // For simplicity, we'll just return a generic error response here.
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching departments."));
            }
        }
        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _unitOfWork.DepartmentRepository.GetAsync(u => u.DepartmentId == id);
                if (department == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department not found."));
                }
                return Ok(ApiResponseHelper.SuccessResponse(department));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching the department."));
            }
        }
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] RequestDepartmentCreationDto reqDeptDto)
        {
            try
            {
                var department = _mapper.Map<Department>(reqDeptDto);

                await _unitOfWork.DepartmentRepository.AddAsync(department);
                await _unitOfWork.Save();
                return Ok(ApiResponseHelper.SuccessResponse(department, System.Net.HttpStatusCode.Created));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while adding the department."));
            }
        }
        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] RequestDepartmentUpdationDto reqDeptDto)
        {
            try
            {
                var existingDepartment = await _unitOfWork.DepartmentRepository.GetAsync(u => u.DepartmentId == id);
                if (existingDepartment == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department not found."));
                }
                existingDepartment.DepartmentName = reqDeptDto.DepartmentName;
                existingDepartment.IsActive = reqDeptDto.IsActive;
                //var department = _mapper.Map<Department>(reqDeptDto);
                _unitOfWork.DepartmentRepository.UpdateAsync(existingDepartment);
                await _unitOfWork.Save();
                return Ok(ApiResponseHelper.SuccessResponse(existingDepartment));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while updating the department."));
            }
        }
        [HttpGet("GetAllDepartmentManagers")]
        public async Task<IActionResult> GetAllDepartmentManagers()
        {
            try
            {
                var departmentManagers = await _unitOfWork.DepartmentManagerAssignmentRepository.GetAllAsync(includeProperties: "Department");
                return Ok(ApiResponseHelper.SuccessResponse(departmentManagers));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching department managers."));
            }
        }
        [HttpPost("AssignDepartmentUser")]
        public async Task<IActionResult> AssignDepartmentUser([FromBody] RequestDepartmentUserAssignmentCreationDto reqDeptUserDto)
        {
            try
            {
                var departmentUser = _mapper.Map<DepartmentUserAssignment>(reqDeptUserDto);
                await _unitOfWork.DepartmentUserAssignmentRepository.AddAsync(departmentUser);
                await _unitOfWork.Save();
                return Ok(ApiResponseHelper.SuccessResponse(departmentUser, HttpStatusCode.Created));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while assigning the department user."));
            }
        }

        [HttpGet("GetDepartmentUserByUserId/{userId}")]
        public async Task<IActionResult> GetDepartmentUserByUserId(string userId)
        {
            try
            {
                var assignment = await _unitOfWork.DepartmentUserAssignmentRepository.GetAsync(u => u.UserId == userId && u.IsActive, "Department,User");
                var user = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == userId);

                if (assignment == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department Manager not found."));
                }
                var result = _mapper.Map<ResponseDepartmentUserAssignmentDto>(assignment);
                if (assignment.UserId != null)
                {
                    result.User = _mapper.Map<UserDetailDto>(assignment.User);

                    result.User.Roles = (await _userManager.GetRolesAsync(assignment.User)).ToList().FirstOrDefault();
                }
                return Ok(ApiResponseHelper.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching the department manager."));
            }
        }
        [HttpGet("GetDepartmentUserByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetDepartmentManagerByDepartmentId(int departmentId)
        {
            try
            {
                var assignment = await _unitOfWork.DepartmentUserAssignmentRepository.GetAllAsync(u => u.DepartmentId == departmentId && u.IsActive, "Department,ManagerUser");
                if (assignment == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department Manager not found."));
                }
                List<ResponseDepartmentUserAssignmentDto> response = new List<ResponseDepartmentUserAssignmentDto>();
                foreach (var item in assignment)
                {
                    var user = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == item.UserId);
                    var result = _mapper.Map<ResponseDepartmentUserAssignmentDto>(assignment);
                    if (item.UserId != null)
                    {
                        result.User = _mapper.Map<UserDetailDto>(item.User);

                        result.User.Roles = (await _userManager.GetRolesAsync(item.User)).ToList().FirstOrDefault();
                    }
                    response.Add(result);
                }
                //var user = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == assignment.ManagerUserId);


                //var result = _mapper.Map<ResponseDepartmentManagerAssignmentDto>(assignment);
                //if (assignment.ManagerUser != null)
                //{
                //    result.ManagerUser = _mapper.Map<ManagerUserDetailDto>(
                //        assignment.ManagerUser
                //    );

                //    result.ManagerUser.Roles = (await _userManager.GetRolesAsync(assignment.ManagerUser)).ToList().FirstOrDefault();
                //}

                return Ok(ApiResponseHelper.SuccessResponse(response));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching the department manager."));
            }
        }

    }
}
