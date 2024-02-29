using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class StudentExam:AuditableEntity
    {
        public StudentExam()
        {
            StudentQuestions = new HashSet<StudentQuestion>();
        }

        public int? Score { get; set; } = 0;
        public bool IsFinished { get; set; } = false;

        //Navigation Prop.
        public Guid ExamId { get; set; }
        public virtual Exam? Exam { get; set; }
        public Guid StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<StudentQuestion> StudentQuestions { get; set; }
    }
}
