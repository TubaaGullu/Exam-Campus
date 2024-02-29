using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentExam
{
    public class StudentExamListDto
    {
        public Guid Id { get; set; }
        public int? Score { get; set; }
        public bool IsFinished { get; set; }
        public string ExamName { get; set; }
        public string MaxScore { get; set; }
        public DateTime ExamDateTime { get; set; }
        public Guid ExamId { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
    }
}
