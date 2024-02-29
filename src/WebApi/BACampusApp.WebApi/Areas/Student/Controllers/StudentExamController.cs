using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;

using BACampusApp.Dtos.StudentAnswer;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Student.Controllers
{
    public class StudentExamController : StudentBaseController
    {
        private readonly IStudentExamService _studentExamService;
        private readonly IMapper _mapper;

        public StudentExamController(IStudentExamService studentExamService, IMapper mapper)
        {
            _mapper = mapper;
            _studentExamService = studentExamService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllFutureExamByStudentIdentityId(string StudentIdentityId)
        {
            var result = await _studentExamService.GetAllFutureExamByStudentIdentityIdAsync(StudentIdentityId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllOldExamByStudentIdentityId(string StudentIdentityId)
        {
            var result = await _studentExamService.GetAllOldExamByStudentIdentityIdAsync(StudentIdentityId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllCurrentExamByStudentIdentityId(string StudentIdentityId)
        {
            var result = await _studentExamService.GetAllCurrentExamByStudentIdentityIdAsync(StudentIdentityId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> StartExam(Guid studentExamId)
        {
            var result = await _studentExamService.ExamStartStudentExamIdAsync(studentExamId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> FinishedExam([FromBody] List<StudentAnswerCreateDto> newStudentAnswerCreateDtos, Guid studentExamId)
        {
            var result = await _studentExamService.ExamFnishedStudentExamIdAsync(newStudentAnswerCreateDtos, studentExamId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}

