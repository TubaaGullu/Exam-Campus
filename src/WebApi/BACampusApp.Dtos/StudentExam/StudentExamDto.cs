using BACampusApp.Dtos.StudentQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentExam
{
    public class StudentExamDto
    {
        public Guid Id { get; set; }
        public int? Score { get; set; }
        public bool IsFinished { get; set; }
        public Guid ExamId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentFullName { get; set; }
    }
}
