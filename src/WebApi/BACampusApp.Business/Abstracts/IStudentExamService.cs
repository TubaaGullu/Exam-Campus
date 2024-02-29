using BACampusApp.Dtos.StudentExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IStudentExamService
    {
        /// <summary>
        /// Idsine göre StudentExami döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<StudentExamDto>> GetByIdAsync(Guid id);


        /// <summary>
        ///  ExamStudent tablosundaki studentta ait gelecek sınavları getirir.
        /// </summary>
        /// <param name="Guid Id">Guid Id</param>
        /// <returns>List<ExamStudentListDto></returns>
        Task<IDataResult<List<StudentExamListDto>>> GetAllFutureExamByStudentIdentityIdAsync(string id);


        /// <summary>
        /// Öğrenciye ait geçmiş sınavların listesini döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<List<StudentExamListDto>>> GetAllOldExamByStudentIdentityIdAsync(string id);


        /// <summary>
        /// Öğrenciye ait mevcut zamandaki sınavların listesini döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<List<StudentExamListDto>>> GetAllCurrentExamByStudentIdentityIdAsync(string id);


        /// <summary>
        /// ExamınIdsine göre StudentExamlistesi döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<List<StudentExamListDto>>> GetAllByExamIdAsync(Guid id);

        /// <summary>
        /// Belirtilen öğrenci sınav kimliğine göre öğrenci sınavını başlatır ve ilgili soruları getirir.
        /// </summary>
        /// <param name="studentExamId">Başlatılacak öğrenci sınavının kimliği.</param>
        /// <returns>
        /// Başarı durumuna göre, öğrenci sınavına ait soruları içeren bir veri sonucu döner.
        /// Başarı durumunda, veri soruların listesiyle birlikte gelir.
        /// Başarısız durumda, hata mesajı içeren bir veri sonucu döner.
        /// </returns>
        Task<IDataResult<List<StudentQuestionDetailsDto>>> ExamStartStudentExamIdAsync(Guid studentExamId);


        /// <summary>
        /// Belirtilen öğrenci sınavının bitiş zamanına göre sınavı tamamlar.Öğrenci cevaplarını kaydeder. Öğrencinin sınav sonucunu hesaplar.
        /// </summary>
        /// <param name="studentExamId">Tamamlanacak öğrenci sınavının kimliği.</param>
        /// <returns>
        /// Başarı durumuna göre, öğrenci sınavının tamamlanma durumunu içeren bir veri sonucu döner.
        /// Başarı durumunda, veri sınavın tamamlandığına dair bilgiyi içerir ve öğrenci sınavının sonucunu hesaplar.
        /// Başarısız durumda, hata mesajı içeren bir veri sonucu döner.
        /// </returns>
        Task<IResult> ExamFnishedStudentExamIdAsync(List<StudentAnswerCreateDto> studentAnswerCreateDtos, Guid studentExamId);
    }
}
