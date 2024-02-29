using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Dtos.QuestionTopic;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Trainer.Controllers
{
    public class QuestionTopicController : TrainerBaseController
    {
        private readonly IQuestionTopicService _questionTopicService;   
        private readonly IMapper _mapper;

        public QuestionTopicController(IMapper mapper, IQuestionTopicService questionTopicService)
        {
            _mapper = mapper;
            _questionTopicService = questionTopicService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] QuestionTopicCreateDto newQuestionTopicCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _questionTopicService.AddAsync(newQuestionTopicCreate);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _questionTopicService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _questionTopicService.GetListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetById(Guid guid)
        {
            var result = await _questionTopicService.GetByIdAsync(guid);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Update([FromBody] QuestionTopicUpdateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _questionTopicService.UpdateAsync(model);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }

        
    }
}

