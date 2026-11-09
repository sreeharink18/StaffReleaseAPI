using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StaffReleaseAPI.Models;
using StaffReleaseAPI.Models.DTOs.StaffReleaseDtoFolder;
using StaffReleaseAPI.Repository.IRepository;
using StaffReleaseAPI.Services.Helper;
using System.Net;

namespace StaffReleaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffReleaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StaffReleaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost("StaffReleaseCreation")]
        public async Task<IActionResult> CreateStaffRelease([FromBody] CreateStaffReleaseRequestDto dto)
        {
            try
            {
                // 1. Get the logged-in user's ID from the token
                if (string.IsNullOrWhiteSpace(dto.CreatedById))
                {
                    return Unauthorized(ApiResponseHelper.ErrorResponse("Unable to identify the logged-in user."));
                }

                // 2. Remove duplicate department IDs
                var departmentIds = dto.RequiredDepartmentIds.Distinct().ToList();

                if (departmentIds.Count == 0)
                {
                    return BadRequest(ApiResponseHelper.ErrorResponse("Select at least one clearance department."));
                }

                // 3. Verify that every selected department exists
                // and is active
                var allDepartments = await _unitOfWork.DepartmentRepository.GetAllAsync();

                var validDepartmentIds = allDepartments
                    .Where(d => d.IsActive && departmentIds.Contains(d.DepartmentId))
                    .Select(d => d.DepartmentId)
                    .ToHashSet();

                if (validDepartmentIds.Count != departmentIds.Count)
                {
                    return BadRequest(ApiResponseHelper.ErrorResponse("One or more selected departments are invalid or inactive."));
                }

                // 4. Find an active manager assignment for each
                // selected department before creating the request
                var managerIdsByDepartment = new Dictionary<int, string>();

                foreach (var departmentId in departmentIds)
                {
                    var assignment = await _unitOfWork.DepartmentUserAssignmentRepository.GetAsync(x => x.DepartmentId == departmentId && x.IsActive && x.IsManager);

                    if (assignment == null || string.IsNullOrWhiteSpace(assignment.UserId))
                    {
                        return BadRequest(ApiResponseHelper.ErrorResponse($"No active manager is assigned to department {departmentId}."));
                    }

                    managerIdsByDepartment[departmentId] = assignment.UserId;
                }

                // 5. Map the request DTO to the entity
                var request = _mapper.Map<StaffReleaseRequest>(dto);

                // Server-controlled properties
                request.Status = StaticData.ReleaseStatus_ClearancePending;
                request.CreatedById = dto.CreatedById;
                request.CreatedAt = DateTime.UtcNow;

                // 6. Create one pending clearance record per department
                foreach (var departmentId in departmentIds)
                {
                    request.RequiredDepartments.Add(
                        new RequestRequiredDepartment
                        {
                            DepartmentId = departmentId,
                            AssignedManagerUserId = managerIdsByDepartment[departmentId],
                            Status = StaticData.ClearanceStatus_Pending
                        });
                }

                // 7. Save the parent and child entity graph
                await _unitOfWork.StaffReleaseRequestRepository.AddAsync(request);

                await _unitOfWork.Save();

                // 8. Map the saved entity to a safe response DTO
                var response = _mapper.Map<StaffReleaseCreatedResponseDto>(request);

                return Ok(ApiResponseHelper.SuccessResponse(response, HttpStatusCode.Created));
            }
            catch (Exception)
            {
                // Log the exception with your application's logger
                // rather than returning internal details to the client.
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponseHelper.ErrorResponse("Something went wrong while creating the staff release request."));
            }
        }
        [HttpGet("MyPendingClearances/{userId}")]
        public async Task<IActionResult> GetMyPendingClearances(string userId)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var clearances = await _unitOfWork.RequestRequiredDepartmentRepository.GetAllAsync(includeProperties: "StaffReleaseRequest,Department");

            var result = clearances.Where(x =>
                    x.AssignedManagerUserId == userId &&
                    x.Status == StaticData.ClearanceStatus_Pending &&
                    x.StaffReleaseRequest.Status != StaticData.ReleaseStatus_Completed &&
                    x.StaffReleaseRequest.Status != StaticData.ReleaseStatus_Cancelled)
                .Select(x => new ResponsePendingClearanceDto
                {
                    RequestRequiredDepartmentId = x.RequestRequiredDepartmentId,

                    RequestId = x.RequestId,

                    EmployeeCode = x.StaffReleaseRequest.EmpCode,

                    EmployeeName = x.StaffReleaseRequest.EmployeeName,

                    DepartmentName = x.Department.DepartmentName,

                    LastWorkingDay = x.StaffReleaseRequest.LastWorkingDay,

                    Status = x.Status
                })
                .ToList();

            return Ok(ApiResponseHelper.SuccessResponse(result));
        }
        [HttpGet("GetRequestDetailsByDepartment/{departmentId}")]
        public async Task<IActionResult> GetRequestDetailsByDepartment(int departmentId)
        {
            var clearances = await _unitOfWork.RequestRequiredDepartmentRepository.GetAllAsync(includeProperties: "StaffReleaseRequest,Department");
            var result = clearances.Where(x =>
                    x.DepartmentId == departmentId &&
                    x.Status == StaticData.ClearanceStatus_Pending &&
                    x.StaffReleaseRequest.Status != StaticData.ReleaseStatus_Completed &&
                    x.StaffReleaseRequest.Status != StaticData.ReleaseStatus_Cancelled)
                .Select(x => new ResponsePendingClearanceDto
                {
                    RequestRequiredDepartmentId = x.RequestRequiredDepartmentId,
                    RequestId = x.RequestId,
                    EmployeeCode = x.StaffReleaseRequest.EmpCode,
                    EmployeeName = x.StaffReleaseRequest.EmployeeName,
                    DepartmentName = x.Department.DepartmentName,
                    LastWorkingDay = x.StaffReleaseRequest.LastWorkingDay,
                    Status = x.Status
                })
                .ToList();
            return Ok(ApiResponseHelper.SuccessResponse(result));
        }
        [HttpGet("GetAllRequestDetailsByUserId/{userId}")]
        public async Task<IActionResult> GetAllRequestDetailsByUserId(string userId)
        {
            var department = await _unitOfWork.DepartmentUserAssignmentRepository.GetAsync(u => u.UserId == userId && u.IsActive, "Department,User");
            if (department == null)
            {
                return NotFound(ApiResponseHelper.ErrorResponse("Department User not found."));
            }
            var staffReleaseDetailByDepId = await _unitOfWork.RequestRequiredDepartmentRepository.GetAllAsync(u => u.DepartmentId == department.DepartmentId, includeProperties: "StaffReleaseRequest,Department");

            //var clearances = await _unitOfWork.RequestRequiredDepartmentRepository.GetAllAsync(includeProperties: "StaffReleaseRequest,Department");

            return Ok(ApiResponseHelper.SuccessResponse(staffReleaseDetailByDepId));
        }
    }
}
