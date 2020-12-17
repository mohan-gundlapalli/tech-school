using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechSchool.Web.DTO;
using TechSchool.Web.Infrastructure;
using TechSchool.Web.Repositories;

namespace TechSchool.Web.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchoolApiController: ControllerBase
    {
        ILogger<SchoolApiController> _logger;
        ISchoolRepository _schoolRepo;
        public SchoolApiController(ILogger<SchoolApiController> logger, ISchoolRepository repo) 
        {
            _logger = logger;
            _schoolRepo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult>> Students() {
            try
            {
                var students = await _schoolRepo.GetStudents();
                var result = new ApiResult
                {
                    Data = students,
                    Success = true
                };

                return result;
            }
            catch (Exception ex)
            {
                var result = new ApiResult
                {
                    Success = false,
                    Message = $"Error in fetching students: {ex.Message}"
                };

                return result;
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> CreateStudent([FromBody] StudentDTO model)
        {
            try
            {
                var student = await _schoolRepo.CreateStudent(model);
                var result = new ApiResult
                {
                    Data = student,
                    Success = true
                };

                return result;
            }
            catch (Exception ex)
            {
                var result = new ApiResult
                {
                    Success = false,
                    Message = $"Error in creating student: {ex.Message}"
                };

                return result;
            }
        }

        
        [HttpPost]
        public async Task<ActionResult<ApiResult>> SaveStudent([FromBody] StudentDTO model)
        {
            try
            {
                var student = await _schoolRepo.Save(model);
                var result = new ApiResult
                {
                    Data = student,
                    Success = true
                };

                return result;
            }
            catch (Exception ex)
            {
                var result = new ApiResult
                {
                    Success = false,
                    Message = $"Error in Saving student: {ex.Message}"
                };

                return result;
            }
        }

        [HttpPost]
        [Route("DeleteStudent/{studentId:int}")]
        public async Task<ActionResult<ApiResult>> DeleteStudent(int studentId)
        {
            try
            {
                var student = await _schoolRepo.Delete(studentId);
                var result = new ApiResult
                {
                    Data = student,
                    Success = true
                };

                return result;
            }
            catch (Exception ex)
            {
                var result = new ApiResult
                {
                    Success = false,
                    Message = $"Error in deleting student: {ex.Message}"
                };

                return result;
            }
        }
    }
}
