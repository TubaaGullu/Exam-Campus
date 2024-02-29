using BACampusApp.Dtos.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IExamService
    {
        /// <summary>
        /// ilgili ıd ile sınav nesnesini döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<SelectedExamDto>> GetByIdAsync(Guid id);


        /// <summary>
        /// Mevcut bütün sınavları liste olarak döner.
        /// <returns>ExamListDto</returns>
        Task<IDataResult<List<ExamListDto>>> GetAllAsync();



        /// <summary>
        /// sınav, sınavın öğrencilerini ve sorularını ekler.
        /// </summary>
        /// <param name="examCreateDto">ExamCreateDto</param>
        /// <returns>DataResult<ExamDto></returns>
        Task<IDataResult<ExamDto>> AddAsync(ExamCreateDto examCreateDto);


        /// <summary>
        /// sınavı, sınavın öğrencilerini ve sorularını siler.
        /// </summary>
        /// <returns>IResult döndürür</returns>
        Task<IResult> DeleteAsync(Guid id);


        /// <summary>
        /// sınav, sınavın öğrencilerini ve sorularını günceller.
        /// </summary>
        /// <param name="examUpdateDto"></param>
        /// <returns></returns>
        Task<IDataResult<ExamDto>> UpdateAsync(ExamUpdateDto examUpdateDto);

        /// <summary>
        /// TrainerIdentityId ye göre eğitmenin eklediği tüm gelecek sınavları döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<ExamListDto>>> GetAllFutureExamByTrainerIdentityIdAsync(string trainerId);

        /// <summary>
        /// TrainerIdentityId ye göre eğitmenin eklediği tüm eski sınavları döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<ExamListDto>>> GetAllOldExamByTrainerIdentityIdAsync(string trainerId);

        /// <summary>
        /// TrainerIdentityId ye göre eğitmenin eklediği anlık sınavları döner.
        /// </summary>
        /// <returns></returns>
        Task<IDataResult<List<ExamListDto>>> GetAllCurrentExamByTrainerIdentityIdAsync(string trainerId);



        /// <summary>
        /// examınIdsine göre examı detaylı olarak döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<ExamDetailDto>> GetExamDetailByIdAsync(Guid id);


    }
}
