using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentQuestion
{
    public class StudentQuestionListDto
    {
        public Guid Id { get; set; }
        public int? Score { get; set; }
        public int QuestionOrder { get; set; }
        public string StudentName { get; set; }
        public string ExamName { get; set; }
    }
}
