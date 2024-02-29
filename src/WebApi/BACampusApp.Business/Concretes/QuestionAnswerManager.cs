using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class QuestionAnswerManager : IQuestionAnswerService
    {
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public QuestionAnswerManager(IQuestionAnswerRepository questionAnswerRepository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _questionAnswerRepository = questionAnswerRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// cevap oluşturur.
        /// </summary>
        /// <param name="questionCreateDto"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionAnswerDto>> AddAsync(QuestionAnswerCreateDto questionAnswerCreateDto)
        {
            var questionAnswer = _mapper.Map<QuestionAnswer>(questionAnswerCreateDto);
            await _questionAnswerRepository.AddAsync(questionAnswer);
            await _questionAnswerRepository.SaveChangesAsync();

            return new SuccessDataResult<QuestionAnswerDto>(_mapper.Map<QuestionAnswerDto>(questionAnswer), _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// cevaplar oluşturur.
        /// </summary>
        /// <param name="questionCreateDto"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionAnswerDto>>> AddRangeAsync(List<QuestionAnswerCreateDto> questionAnswersCreateDto)
        {
            var questionAnswers = new List<QuestionAnswer>();

            foreach (var questionAnswerCreateDto in questionAnswersCreateDto)
            {
                var questionAnswer = _mapper.Map<QuestionAnswer>(questionAnswerCreateDto);

                await _questionAnswerRepository.AddAsync(questionAnswer);

                questionAnswers.Add(questionAnswer);
            }
            await _questionAnswerRepository.SaveChangesAsync();

            return new SuccessDataResult<List<QuestionAnswerDto>>(_mapper.Map<List<QuestionAnswerDto>>(questionAnswers), _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// cevabı siler.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var questionAnswer = await _questionAnswerRepository.GetByIdAsync(id);

            if (questionAnswer is null)
            {
                return new ErrorResult(_stringLocalizer[Messages.QuestionAnswerNotFound]);
            }

            await _questionAnswerRepository.DeleteAsync(questionAnswer);
            await _questionAnswerRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// cevapları siler.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteRangeAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var questionAnswer = await _questionAnswerRepository.GetByIdAsync(id);

                if (questionAnswer is null)
                {
                    return new ErrorResult(_stringLocalizer[Messages.QuestionAnswerNotFound]);
                }

                await _questionAnswerRepository.DeleteAsync(questionAnswer);
            }

            await _questionAnswerRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// Id ye göre ilgili cevabı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionAnswerDto>> GetById(Guid id)
        {
            var questionAnswer = await _questionAnswerRepository.GetByIdAsync(id);
            if (questionAnswer == null)
            {
                return new ErrorDataResult<QuestionAnswerDto>(_stringLocalizer[Messages.QuestionAnswerNotFound]);
            }

            return new SuccessDataResult<QuestionAnswerDto>(_mapper.Map<QuestionAnswerDto>(questionAnswer), _stringLocalizer[Messages.QuestionAnswerFoundSuccess]);
        }
        /// <summary>
        ///  cevapları günceller.
        /// </summary>
        /// <param name="questionupdatedto"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionAnswerDto>>> UpdateRangeAsync(List<QuestionAnswerUpdateDto> questionAnswersUpdateDto)
        {

            if (questionAnswersUpdateDto.Count > 0)
            {
                var currentQuestionAnswers = await _questionAnswerRepository.GetAllAsync(x => x.QuestionId == questionAnswersUpdateDto[0].QuestionId);

                foreach (var updateDto in questionAnswersUpdateDto)
                {
                    var currentQuestionAnswer = currentQuestionAnswers.FirstOrDefault(x => x.Id == updateDto.Id);

                    if (currentQuestionAnswer != null)
                    {
                        var updatedQuestion = _mapper.Map(updateDto, currentQuestionAnswer);
                        await _questionAnswerRepository.UpdateAsync(updatedQuestion);
                    }
                }
            }
            var updatedQuestionAnswers = await _questionAnswerRepository.GetAllAsync(x => x.QuestionId == questionAnswersUpdateDto[0].QuestionId);
            var updatedQuestionAnswerDtos = _mapper.Map<List<QuestionAnswerDto>>(updatedQuestionAnswers);

            return new SuccessDataResult<List<QuestionAnswerDto>>(updatedQuestionAnswerDtos, _stringLocalizer[Messages.UpdateSuccess]);

        }
    }
}
