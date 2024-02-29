using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.DataAccess.Interfaces.Repositories;

using BACampusApp.Dtos.Exam;

using BACampusApp.Dtos.Question;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class ExamController : TrainerBaseController
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public ExamController(IExamService examService, IMapper mapper) 
        {
            _examService = examService;
            _mapper = mapper;

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] ExamCreateDto newExamCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _examService.AddAsync(newExamCreate);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _examService.DeleteAsync(id);
            return deleteResult.IsSuccess == true ? Ok(deleteResult) : BadRequest(ModelState);

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllFutureExamByTrainerId(string identityTrainerId)
        {
            var result = await _examService.GetAllFutureExamByTrainerIdentityIdAsync(identityTrainerId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllOldExamByTrainerId(string identityTrainerId)
        {
            var result = await _examService.GetAllOldExamByTrainerIdentityIdAsync(identityTrainerId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllCurrentExamByTrainerId(string identityTrainerId)
        {
            var result = await _examService.GetAllCurrentExamByTrainerIdentityIdAsync(identityTrainerId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetExamforSelectedQuestionandSelectedStudentByExamId(Guid id)
        {
            var result = await _examService.GetByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetExamDetailByExamId(Guid id)
        {
            var result = await _examService.GetExamDetailByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] ExamUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _examService.UpdateAsync(model);
            return updateResult.IsSuccess == true ? Ok(updateResult) : BadRequest(updateResult);
        }

        
    }
}

