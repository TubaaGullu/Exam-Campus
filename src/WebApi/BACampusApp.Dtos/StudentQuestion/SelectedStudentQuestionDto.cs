using BACampusApp.Dtos.StudentAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentQuestion
{
    public class SelectedStudentQuestionDto
    {
        //public Guid Id { get; set; }
        public int? Score { get; set; }
        public int QuestionOrder { get; set; }
        //public Guid? StudentExamId { get; set; }
        public Guid? QuestionId { get; set; }
    }
}
