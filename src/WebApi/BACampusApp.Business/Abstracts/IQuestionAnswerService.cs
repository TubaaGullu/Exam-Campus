using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IQuestionAnswerService
    {
        /// <summary>
        /// Id ye göre ilgili cevabı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<QuestionAnswerDto>> GetById(Guid id);
        /// <summary>
        /// cevap oluşturur.
        /// </summary>
        /// <param name="questionCreateDto"></param>
        /// <returns></returns>
        Task<IDataResult<QuestionAnswerDto>> AddAsync(QuestionAnswerCreateDto questionAnswerCreateDto);
        /// <summary>
        /// cevaplar oluşturur.
        /// </summary>
        /// <param name="questionCreateDto"></param>
        /// <returns></returns>
        Task<IDataResult<List<QuestionAnswerDto>>> AddRangeAsync(List<QuestionAnswerCreateDto> questionAnswersCreateDto);

        /// <summary>
        ///  cevapları günceller.
        /// </summary>
        /// <param name="questionUpdateDto"></param>
        /// <returns></returns>
        Task<IDataResult<List<QuestionAnswerDto>>> UpdateRangeAsync(List<QuestionAnswerUpdateDto> questionAnswersUpdateDto);

        /// <summary>
        /// cevabı siler.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// cevapları siler.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<IResult> DeleteRangeAsync(List<Guid> ids);
    }
}
