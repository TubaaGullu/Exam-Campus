using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class QuestionTopicController : AdminBaseController
    {
        private readonly IQuestionTopicService _questionTopicService;
        private readonly IMapper _mapper;
        public QuestionTopicController(IMapper mapper, IQuestionTopicService questionTopicService)
        {
            _mapper = mapper;
            _questionTopicService = questionTopicService;
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
        [Authorize(Roles = Roles.AdminRole)] 
        public async Task<IActionResult> DeletedListAll()
        {
            var result = await _questionTopicService.DeletedListAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}
