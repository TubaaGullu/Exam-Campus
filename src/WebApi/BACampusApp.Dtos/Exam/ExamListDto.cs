using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Exam
{
    public class ExamListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ExamDateTime { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public bool? IsStarted { get; set; } = false;
        public int MaxScore { get; set; }
    }
}
