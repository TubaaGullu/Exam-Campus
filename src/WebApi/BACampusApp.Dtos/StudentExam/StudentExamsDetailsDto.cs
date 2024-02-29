using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentExam
{
    public class StudentExamsDetailsDto
    {

        public Guid Id { get; set; }
        public int? Score { get; set; }
        public string ExamName { get; set; }
        public DateTime ExamDateTime { get; set; }
        public string StudentFullName { get; set; }
    }
}
