using BACampusApp.Dtos.QuestionAnswer;
using BACampusApp.Dtos.StudentAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentQuestion
{
    public class StudentQuestionDetailsDto
    {
        public Guid Id { get; set; }
        public int QuestionOrder { get; set; }
        public int Score { get; set; }
        public string QuestionText { get; set; }
        public string? Image { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public DateTime ExamDateTime { get; set; }
        public Guid? StudentExamId { get; set; }
        public Guid? QuestionId { get; set; }
        public List<QuestionAnswerForStudentDto> QuestionAnswers { get; set; }
    }
}
