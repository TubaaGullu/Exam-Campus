using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Core.Utilities.Results;
using BACampusApp.Dtos.Question;
using BACampusApp.Dtos.QuestionAnswer;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class QuestionController :TrainerBaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;
        public QuestionController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateQuestionAndAnswers([FromBody] QuestionCreateDto newQuestionCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _questionService.AddAsync(newQuestionCreate);
            return result.IsSuccess == true ? Ok(result) : BadRequest(result);
        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateQuestionAndAnswers([FromBody] QuestionUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateResult = await _questionService.UpdateAsync(model);
            return updateResult.IsSuccess == true ? Ok(updateResult) : BadRequest(updateResult);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteQuestionAndAnswers(Guid id)
        {
            var deleteResult = await _questionService.DeleteAsync(id);
            return deleteResult.IsSuccess == true ? Ok(deleteResult) : BadRequest(ModelState);

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllQuestionAndAnswer()
        {
            var result = await _questionService.GetAllQuestionAndAnswerAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllQuestion()
        {
            var result = await _questionService.GetAllQuestionAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllQuestionByTrainerId(string id)
        {
            var result = await _questionService.GetAllQuestionByTrainerIdentityIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAllQuestionAndAnswerByTrainerId(string id)
        {
            var result = await _questionService.GetAllQuestionAndAnswerByTrainerIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetQuestionByQuestionId(Guid id)
        {
            var result = await _questionService.GetQuestionByQuestionIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetQuestionAndAnswerByQuestionId(Guid id)
        {
            var result = await _questionService.GetQuestionAndAnswersByQuestionIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetQuestionByTopicId(Guid id)
        {
            var result = await _questionService.GetQuestionByTopicIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetQuestionAndAnswerByTopicId(Guid id)
        {
            var result = await _questionService.GetQuestionAndAnswersByTopicIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
