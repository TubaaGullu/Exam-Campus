global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
global using BACampusApp.Business.Abstracts;
global using BACampusApp.Business.Constants;
global using BACampusApp.Business.Concretes;
global using BACampusApp.Core.Utilities.Results;
global using BACampusApp.DataAccess.Interfaces.Repositories;
global using BACampusApp.Dtos.Admin;
global using BACampusApp.Entities.DbSets;

global using IResult = BACampusApp.Core.Utilities.Results.IResult;
global using Microsoft.AspNetCore.Http;

global using System.Net.Mail;
global using System.Net;
global using Microsoft.Extensions.Options;

global using Microsoft.AspNetCore.Hosting;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using PhoneNumbers;
global using BACampusApp.Dtos.Students;

global using BACampusApp.Dtos.Trainers;
global using Microsoft.AspNetCore.Identity;
global using BACampusApp.Entities.Enums;
global using Microsoft.Extensions.Localization;
global using BACampusApp.Business.TypedHttpClients;
global using BACampusApp.Core.Enums;

global using BACampusApp.Core.Enums;

global using BACampusApp.Dtos.Question;
global using BACampusApp.Dtos.QuestionAnswer;
global using BACampusApp.Dtos.Exam;
global using BACampusApp.Dtos.QuestionTopic;
global using BACampusApp.Dtos.StudentQuestion;
global using BACampusApp.Dtos.StudentAnswer;
global using BACampusApp.Dtos.StudentExam;