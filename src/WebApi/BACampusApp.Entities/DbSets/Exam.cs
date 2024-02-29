using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class Exam : AuditableEntity
    {
        public Exam()
        {
            StudentExams = new HashSet<StudentExam>();
            
        }
        public string Name { get; set; }
        public DateTime ExamDateTime { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string? Description { get; set; }
        public bool? IsStarted { get; set; }=false;
        public int MaxScore { get; set; } = 100;
        public virtual ICollection<StudentExam> StudentExams { get; set; }

    }
}
