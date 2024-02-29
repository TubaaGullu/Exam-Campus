using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class QuestionTopicManager :IQuestionTopicService
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IQuestionTopicRepository _questionTopicRepository;
        public QuestionTopicManager(IStringLocalizer<Resource> stringLocalizer, IQuestionTopicRepository questionTopicRepository, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _questionTopicRepository = questionTopicRepository;
            _mapper = mapper;
        }
        /// <summary>
        ///  Bu metot QuestionTopic nesnesi oluşturma işlemini işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="newSubject"></param>
        ///  <returns>></returns>
        public async Task<IDataResult<QuestionTopicDto>> AddAsync(QuestionTopicCreateDto newQuestionTopic)
        {
            string inputNameWithoutSpaces = newQuestionTopic.TopicName.Replace(" ", string.Empty).ToLower();

            bool hasTopic = await _questionTopicRepository.AnyAsync(s => s.TopicName.Replace(" ", string.Empty).ToLower() == inputNameWithoutSpaces);

            if (hasTopic)
                return new ErrorDataResult<QuestionTopicDto>(_stringLocalizer[Messages.QuestionTopicAlreadyExists]);
            var questionTopic = await _questionTopicRepository.AddAsync(_mapper.Map<QuestionTopic>(newQuestionTopic));
            await _questionTopicRepository.SaveChangesAsync();
            return new SuccessDataResult<QuestionTopicDto>(_mapper.Map<QuestionTopicDto>(questionTopic), _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        ///  Bu metot QuestionTopic nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen QuestionTopic nesnesinin Guid tipinde Id si </param>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingQuestionTopic = await _questionTopicRepository.GetByIdAsync(id);
            if (deletingQuestionTopic == null)
                return new ErrorResult(_stringLocalizer[Messages.QuestionTopicNotFound]);
            await _questionTopicRepository.DeleteAsync(deletingQuestionTopic);
            await _questionTopicRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        ///  Bu metot ,silinen tüm QuestionTopic nesnelerinin listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>    
        public async Task<IDataResult<List<QuestionTopicDeletedListDto>>> DeletedListAsync()
        {
            var questionTopics = await _questionTopicRepository.GetAllDeletedAsync();
            if (questionTopics.Count() <= 0)
            {
                return new ErrorDataResult<List<QuestionTopicDeletedListDto>>(_stringLocalizer[Messages.ListedFail]);
            }
            var sortedDeletedQuestionTopics = questionTopics.OrderByDescending(x => x.DeletedDate).ToList();
            return new SuccessDataResult<List<QuestionTopicDeletedListDto>>(_mapper.Map<List<QuestionTopicDeletedListDto>>(sortedDeletedQuestionTopics), _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        ///  Idye göre QuestionTopic nesnesini getirir.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen QuestionTopic nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionTopicDto>> GetByIdAsync(Guid id)
        {
            var questionTopic = await _questionTopicRepository.GetByIdAsync(id);
            if (questionTopic == null)
                return new ErrorDataResult<QuestionTopicDto>(_stringLocalizer[Messages.QuestionTopicNotFound]);
            var questionTopicDto = _mapper.Map<QuestionTopicDto>(questionTopic);
            return new SuccessDataResult<QuestionTopicDto>(questionTopicDto, _stringLocalizer[Messages.QuestionTopicFoundSuccess]);
        }

        /// <summary>
        ///  Bu metot ,tüm QuestionTopic listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>  
        public async Task<IDataResult<List<QuestionTopicListDto>>> GetListAsync()
        {
            var questionTopics = await _questionTopicRepository.GetAllAsync();
            if (questionTopics.Count() <= 0)
                return new ErrorDataResult<List<QuestionTopicListDto>>(_stringLocalizer[Messages.ListHasNoElements]);
            var sortedQuestionTopics = questionTopics.OrderByDescending(x => x.CreatedDate).ToList();
            return new SuccessDataResult<List<QuestionTopicListDto>>(_mapper.Map<List<QuestionTopicListDto>>(sortedQuestionTopics), _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot QuestionTopic nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IDataResult<QuestionTopicDto>> UpdateAsync(QuestionTopicUpdateDto updatedQuestionTopic)
        {
            var questionTopic = await _questionTopicRepository.GetByIdAsync(updatedQuestionTopic.Id);

            if (questionTopic == null)
                return new ErrorDataResult<QuestionTopicDto>(_stringLocalizer[Messages.QuestionTopicNotFound]);

            string inputNameWithoutSpaces = updatedQuestionTopic.TopicName.Replace(" ", string.Empty).ToLower();
            bool hasQuestionTopic = await _questionTopicRepository.AnyAsync(s => s.TopicName.Replace(" ", string.Empty).ToLower() == inputNameWithoutSpaces);

            if (questionTopic.TopicName != updatedQuestionTopic.TopicName)
            {
                if (hasQuestionTopic)
                    return new ErrorDataResult<QuestionTopicDto>(_stringLocalizer[Messages.QuestionTopicAlreadyExists]);
            }
            await _questionTopicRepository.UpdateAsync(_mapper.Map(updatedQuestionTopic, questionTopic));
            await _questionTopicRepository.SaveChangesAsync();

            return new SuccessDataResult<QuestionTopicDto>(_stringLocalizer[Messages.UpdateSuccess]);
        }

    }
}
