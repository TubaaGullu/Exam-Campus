using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Business.Constants;

using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections;

namespace BACampusApp.WebApi.Areas.Admin.Controllers
{
    public class ExamController : AdminBaseController
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public ExamController(IExamService examService, IMapper mapper)
        {
            _examService = examService;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> ListAll()
        {
            var result = await _examService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}

