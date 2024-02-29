using BACampusApp.Dtos.StudentAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IStudentAnswerService
    {
        /// <summary>
        /// Öğrencinin vermiş olduğu cevapları database liste olarak kaydetmek için kullanılır.
        /// </summary>
        /// <param name="studentAnswerCreateDtos">List<AnswerOfStudentDto></param>
        /// <returns>DataResult tipinde öğrencinin cevapları.</returns>
        Task<IDataResult<List<StudentAnswerDto>>> AddRangeAsync(List<StudentAnswerCreateDto> studentAnswerCreateDtos);

        /// <summary>
        /// Belirli öğrenci soru kimliklerine göre öğrenci cevaplarını getirir ve cevapların doğruluk durumunu ayarlar.
        /// </summary>
        /// <param name="studentQuestionIds">Öğrenci soru kimlikleri.</param>
        /// <returns>Öğrenci cevaplarını içeren bir veri sonucu.</returns>
        Task<IDataResult<List<StudentAnswerDto>>> GetByStudentQuestionIds(List<Guid> studentQuestionIds);
    }
}
