using BACampusApp.Business.Abstracts;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IMapper _mapper;


        public QuestionManager(IQuestionRepository questionRepository, IMapper mapper, IQuestionAnswerRepository questionAnswerRepository, ITrainerRepository trainerRepository, IStringLocalizer<Resource> stringLocalizer)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _questionAnswerRepository = questionAnswerRepository;
            _trainerRepository = trainerRepository;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// soru ve cevaplarını oluşturur.
        /// </summary>
        /// <param name="questionCreateDto"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionAndAnswersDto>> AddAsync(QuestionCreateDto questionCreateDto)
        {
            var question = _mapper.Map<Question>(questionCreateDto);

            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();

            return new SuccessDataResult<QuestionAndAnswersDto>(_mapper.Map<QuestionAndAnswersDto>(question), _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// soru ve cevaplarını siler.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId, false);

            if (question is null)
            {
                return new ErrorResult(_stringLocalizer[Messages.QuestionNotFound]);
            }
            var questionAnswers = await _questionAnswerRepository.GetAllAsync(x => x.QuestionId == question.Id);
            question.QuestionAnswers = questionAnswers.ToList();
            await _questionRepository.DeleteAsync(question);
            await _questionRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// tüm soru ve cevaplarını döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionAndAnswerListDto>>> GetAllQuestionAndAnswerAsync()
        {
            var questionAndOptions = await _questionRepository.GetAllAsync();
            if (questionAndOptions is null)
            {
                return new ErrorDataResult<List<QuestionAndAnswerListDto>>(_stringLocalizer[Messages.ListedFail]);
            }
            var questionAndOptionListDto = _mapper.Map<List<QuestionAndAnswerListDto>>(questionAndOptions);
            return new SuccessDataResult<List<QuestionAndAnswerListDto>>(questionAndOptionListDto, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// TrainerId ye göre eğitmenin eklediği tüm soruları ve seçeneklerini döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionAndAnswerListDto>>> GetAllQuestionAndAnswerByTrainerIdAsync(string trainerId)
        {
            var trainer = _trainerRepository.GetAsync(x => x.IdentityId == trainerId).Result;
            if (trainer == null)
            {
                return new ErrorDataResult<List<QuestionAndAnswerListDto>>(_stringLocalizer[Messages.TrainerNotFound]);
            }
            var trainerQuestionAndAnswers = await _questionRepository.GetAllAsync(s => s.CreatedBy == trainer.IdentityId.ToString());

            if (trainerQuestionAndAnswers.Count()<=0)
            {
                return new ErrorDataResult<List<QuestionAndAnswerListDto>>(_stringLocalizer[Messages.QuestionNotFound]);
            }

            var trainerQuestionAndAnswersDto = _mapper.Map<List<QuestionAndAnswerListDto>>(trainerQuestionAndAnswers);

            return new SuccessDataResult<List<QuestionAndAnswerListDto>>(trainerQuestionAndAnswersDto, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// tüm soruları döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionListDto>>> GetAllQuestionAsync()
        {
            var questions = await _questionRepository.GetAllAsync();
            if (questions.Count()<0)
            {
                return new ErrorDataResult<List<QuestionListDto>>(_stringLocalizer[Messages.ListedFail]);
            }
            var questionListDto = _mapper.Map<List<QuestionListDto>>(questions);
            return new SuccessDataResult<List<QuestionListDto>>(questionListDto, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// TrainerId ye göre eğitmenin eklediği tüm soruları döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionListDto>>> GetAllQuestionByTrainerIdentityIdAsync(string trainerId)
        {
            var trainer = _trainerRepository.GetAsync(x => x.IdentityId == trainerId).Result;
            if (trainer == null)
            {
                return new ErrorDataResult<List<QuestionListDto>>(_stringLocalizer[Messages.TrainerNotFound]);
            }
            var trainerQuestions = await _questionRepository.GetAllAsync(s => s.CreatedBy == trainer.IdentityId.ToString());

            if (trainerQuestions.Count()<=0)
            {
                return new ErrorDataResult<List<QuestionListDto>>(_stringLocalizer[Messages.QuestionNotFound]);
            }

            var trainerQuestionsDto = _mapper.Map<List<QuestionListDto>>(trainerQuestions);

            return new SuccessDataResult<List<QuestionListDto>>(trainerQuestionsDto, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Sorunun Id'sine göre ilgili soruyu ve seçeneklerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionAndAnswersDto>> GetQuestionAndAnswersByQuestionIdAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id);

            if (question == null)
            {
                return new ErrorDataResult<QuestionAndAnswersDto>(Messages.QuestionNotFound);
            }

            return new SuccessDataResult<QuestionAndAnswersDto>(_mapper.Map<QuestionAndAnswersDto>(question), _stringLocalizer[Messages.QuestionSuccess]);
        }
        /// <summary>
        /// Konunun Idsine göre ilgili soru ve cevapları döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IDataResult<List<QuestionAndAnswerListDto>>> GetQuestionAndAnswersByTopicIdAsync(Guid id)
        {
   
            var questions = await _questionRepository.GetAllAsync(s => s.QuestionTopicId == id);

            if (questions.Count() <= 0)
            {
                return new ErrorDataResult<List<QuestionAndAnswerListDto>>(_stringLocalizer[Messages.QuestionNotFound]);
            }

            var questionsDto = _mapper.Map<List<QuestionAndAnswerListDto>>(questions);

            return new SuccessDataResult<List<QuestionAndAnswerListDto>>(questionsDto, _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// SorununId'sine göre ilgili soruyu getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionDto>> GetQuestionByQuestionIdAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id);

            if (question == null)
            {
                return new ErrorDataResult<QuestionDto>(_stringLocalizer[Messages.QuestionNotFound]);
            }

            return new SuccessDataResult<QuestionDto>(_mapper.Map<QuestionDto>(question), _stringLocalizer[Messages.QuestionSuccess]);
        }
        /// <summary>
        /// KonuId'sine göre ilgili soruları getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<QuestionListDto>>> GetQuestionByTopicIdAsync(Guid id)
        {
            var questions = await _questionRepository.GetAllAsync(x=>x.QuestionTopicId==id);

            if (questions.Count()<=0)
            {
                return new ErrorDataResult<List<QuestionListDto>>(_stringLocalizer[Messages.ListedFail]);
            }

            return new SuccessDataResult<List<QuestionListDto>>(_mapper.Map<List<QuestionListDto>>(questions), _stringLocalizer[Messages.QuestionSuccess]);
        }

        /// <summary>
        /// Sorunun Id'sine göre ilgili soruyu ve seçeneklerini günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionAndAnswersDto>> UpdateAsync(QuestionUpdateDto questionUpdateDto)
        {
            var question = await _questionRepository.GetByIdAsync(questionUpdateDto.Id);

            // Önceki QuestionAnswers listesini sakla
            var existingQuestionAnswers = question.QuestionAnswers.ToList();

            // Mapleme işlemi
            var updatedQuestion = _mapper.Map(questionUpdateDto, question);

            // Yeni QuestionAnswers'ları eski listeyle birleştir
            updatedQuestion.QuestionAnswers = MergeQuestionAnswers(existingQuestionAnswers, updatedQuestion.QuestionAnswers.ToList());

            // Diğer işlemleri yap
            await _questionRepository.UpdateAsync(updatedQuestion);
            await _questionRepository.SaveChangesAsync();

            return new SuccessDataResult<QuestionAndAnswersDto>(_mapper.Map<QuestionAndAnswersDto>(updatedQuestion), _stringLocalizer[Messages.UpdateSuccess]);
        }
        private List<QuestionAnswer> MergeQuestionAnswers(List<QuestionAnswer> existingList, List<QuestionAnswer> updatedList)
        {
            var mergedList = new List<QuestionAnswer>();

            // Eski liste öğelerini ekle
            mergedList.AddRange(existingList);

            // Yeni liste öğelerini ekle
            foreach (var updatedItem in updatedList)
            {
                var existingItem = existingList.FirstOrDefault(e => e.Id == updatedItem.Id);

                // Eğer öğe zaten varsa, güncelle
                if (existingItem != null)
                {
                    // Burada güncelleme işlemlerini yapabilirsiniz.
                    existingItem.Answer = updatedItem.Answer;
                    existingItem.IsRightAnswer = updatedItem.IsRightAnswer;
                }
                else
                {
                    // Eğer öğe yoksa, yeni öğe olarak ekle
                    mergedList.Add(updatedItem);
                }
            }

            return mergedList;
        }
    }
}
