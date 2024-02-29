using BACampusApp.Dtos.StudentExam;
using BACampusApp.Dtos.StudentQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Exam
{
    public class SelectedExamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public bool IsStarted { get; set; }
        public DateTime ExamDateTime { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string? Description { get; set; }
        public int MaxScore { get; set; }
        public ICollection<SelectedStudentQuestionDto> StudentQuestions { get; set; }
        public ICollection<SelectedStudentExamDto> StudentExams { get; set; }
    }
}
