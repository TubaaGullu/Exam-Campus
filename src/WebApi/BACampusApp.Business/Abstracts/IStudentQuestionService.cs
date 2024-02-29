using BACampusApp.Dtos.StudentQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Abstracts
{
    public interface IStudentQuestionService
    {
        /// <summary>
        /// id sine göre studentquestion döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<StudentQuestionDto>> GetByIdAsync(Guid id);

        /// <summary>
        /// studentexamIdye göre öğrencilerin sorularını döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IDataResult<List<StudentQuestionListDto>>> GetByStudentExamIdAsync(Guid id);
        
    }
}
