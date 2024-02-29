using BACampusApp.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IQuestionTopicService
    {
        /// <summary>
        ///  Bu metot QuestionTopic nesnesi oluşturma işlemini işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="newSubject"></param>
        ///  <returns>></returns>
        Task<IDataResult<QuestionTopicDto>> AddAsync(QuestionTopicCreateDto newQuestionTopic);

        /// <summary>
        ///  Bu metot QuestionTopic nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen QuestionTopic nesnesinin Guid tipinde Id si </param>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        ///  Bu metot ,tüm QuestionTopic listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>       
        Task<IDataResult<List<QuestionTopicListDto>>> GetListAsync();

        /// <summary>
        ///  Idye göre QuestionTopic nesnesini getirir.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen QuestionTopic nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        Task<IDataResult<QuestionTopicDto>> GetByIdAsync(Guid id);

        /// <summary>
        /// Bu metot QuestionTopic nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IDataResult<QuestionTopicDto>> UpdateAsync(QuestionTopicUpdateDto updatedQuestionTopic);

        /// <summary>
        ///  Bu metot ,silinen tüm QuestionTopic nesnelerinin listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IDataResult<List<QuestionTopicDeletedListDto>>> DeletedListAsync();
    }
}
