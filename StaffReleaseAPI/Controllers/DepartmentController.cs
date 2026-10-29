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
        [HttpGet("GetDepartmentManagerByUserId/{userId}")]
        public async Task<IActionResult> GetDepartmentManagerByUserId(string userId)
        {
            try
            {
                var assignment = await _unitOfWork.DepartmentManagerAssignmentRepository.GetAsync(u => u.ManagerUserId == userId && u.IsActive, "Department,ManagerUser");
                var user = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == userId);

                if (assignment == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department Manager not found."));
                }
                var result = _mapper.Map<ResponseDepartmentManagerAssignmentDto>(assignment);
                if (assignment.ManagerUser != null)
                {
                    result.ManagerUser = _mapper.Map<ManagerUserDetailDto>(
                        assignment.ManagerUser
                    );

                    result.ManagerUser.Roles = (await _userManager.GetRolesAsync(assignment.ManagerUser)).ToList().FirstOrDefault();
                }
                return Ok(ApiResponseHelper.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching the department manager."));
            }
        }
        [HttpGet("GetDepartmentManagerByDepartmentId/{departmentId}")]
        public async Task<IActionResult> GetDepartmentManagerByDepartmentId(int departmentId)
        {
            try
            {
                var assignment = await _unitOfWork.DepartmentManagerAssignmentRepository.GetAsync(u => u.DepartmentId == departmentId && u.IsActive, "Department,ManagerUser");
                if (assignment == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department Manager not found."));
                }
                var user = await _unitOfWork.ApplicationUserRepository.GetAsync(u => u.Id == assignment.ManagerUserId);


                var result = _mapper.Map<ResponseDepartmentManagerAssignmentDto>(assignment);
                if (assignment.ManagerUser != null)
                {
                    result.ManagerUser = _mapper.Map<ManagerUserDetailDto>(
                        assignment.ManagerUser
                    );

                    result.ManagerUser.Roles = (await _userManager.GetRolesAsync(assignment.ManagerUser)).ToList().FirstOrDefault();
                }

                return Ok(ApiResponseHelper.SuccessResponse(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while fetching the department manager."));
            }
        }
        [HttpPost("AssignDepartmentManager")]
        public async Task<IActionResult> AssignDepartmentManager([FromBody] RequestDepartmentManagerAssignmentDto reqDeptManagerDto)
        {
            try
            {
                var departmentManager = _mapper.Map<DepartmentManagerAssignment>(reqDeptManagerDto);
                await _unitOfWork.DepartmentManagerAssignmentRepository.AddAsync(departmentManager);
                await _unitOfWork.Save();
                return Ok(ApiResponseHelper.SuccessResponse(departmentManager, HttpStatusCode.Created));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while assigning the department manager."));
            }
        }
        [HttpPut("UpdateAssignDepartmentManager/{id}")]
        public async Task<IActionResult> UpdateDepartmentManager(int id, [FromBody] RequestDepartmentManagerAssignmentUpdateDto reqDeptManagerUpdateDto)
        {
            try
            {
                var existingDepartmentManager = await _unitOfWork.DepartmentManagerAssignmentRepository.GetAsync(d => d.AssignmentId == id);
                if (existingDepartmentManager == null)
                {
                    return NotFound(ApiResponseHelper.ErrorResponse("Department Manager not found."));
                }


                if (!string.IsNullOrEmpty(reqDeptManagerUpdateDto.ManagerUserId) || existingDepartmentManager.ManagerUserId != reqDeptManagerUpdateDto.ManagerUserId)
                {
                    existingDepartmentManager.ManagerUserId = reqDeptManagerUpdateDto.ManagerUserId;
                }
                if (!string.IsNullOrEmpty(reqDeptManagerUpdateDto.AssignedById) || existingDepartmentManager.AssignedById != reqDeptManagerUpdateDto.AssignedById)
                {
                    existingDepartmentManager.AssignedById = reqDeptManagerUpdateDto.AssignedById;
                }

                if (reqDeptManagerUpdateDto.IsActive.HasValue && existingDepartmentManager.IsActive != reqDeptManagerUpdateDto.IsActive.Value)
                {
                    existingDepartmentManager.IsActive = reqDeptManagerUpdateDto.IsActive.Value;
                }
                existingDepartmentManager.AssignedAt = DateTime.UtcNow;


                await _unitOfWork.DepartmentManagerAssignmentRepository.UpdateAsync(existingDepartmentManager);
                await _unitOfWork.Save();
                return Ok(ApiResponseHelper.SuccessResponse(existingDepartmentManager));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponseHelper.ErrorResponse("something went wrong while updating the department manager."));
            }
        }
    }
}
