

namespace BACampusApp.Business.Abstracts
{
    public interface IQuestionService
    {
        /// <summary>
        /// KonununId'sine göre ilgili soruyu ve seçeneklerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<List<QuestionAndAnswerListDto>>> GetQuestionAndAnswersByTopicIdAsync(Guid id);

        /// <summary>
        /// Konun Id'sine göre ilgili soruları getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<List<QuestionListDto>>> GetQuestionByTopicIdAsync(Guid id);
        /// <summary>
        /// SorununId'sine göre ilgili soruyu ve seçeneklerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<QuestionAndAnswersDto>> GetQuestionAndAnswersByQuestionIdAsync(Guid id);

        /// <summary>
        /// SorununId'sine göre ilgili soruyu getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<QuestionDto>> GetQuestionByQuestionIdAsync(Guid id);


        /// <summary>
        /// tüm soru ve cevaplarını döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<QuestionAndAnswerListDto>>> GetAllQuestionAndAnswerAsync();

        /// <summary>
        /// soru ve cevap oluşturur.
        /// </summary>
        /// <param name="questionCreateDto"></param>
        /// <returns></returns>

        Task<IDataResult<QuestionAndAnswersDto>> AddAsync(QuestionCreateDto questionCreateDto);

        /// <summary>
        /// soru ve cevapları günceller.
        /// </summary>
        /// <param name="questionUpdateDto"></param>
        /// <returns></returns>
        Task<IDataResult<QuestionAndAnswersDto>> UpdateAsync(QuestionUpdateDto questionUpdateDto);

        /// <summary>
        /// soru ve cevaplarını siler.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid questionId);

        /// <summary>
        /// tüm soruları döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<QuestionListDto>>> GetAllQuestionAsync();

        /// <summary>
        /// TrainerId ye göre eğitmenin eklediği tüm soruları döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<QuestionListDto>>> GetAllQuestionByTrainerIdentityIdAsync(string trainerId);


        /// <summary>
        /// TrainerId ye göre eğitmenin eklediği tüm soruları ve seçeneklerini döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<QuestionAndAnswerListDto>>> GetAllQuestionAndAnswerByTrainerIdAsync(string trainerId);

    }
}
