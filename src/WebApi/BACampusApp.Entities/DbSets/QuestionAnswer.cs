using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class QuestionAnswer : AuditableEntity
    {
        public QuestionAnswer()
        {
            StudentAnswers = new HashSet<StudentAnswer>();
        }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; } 

        public string Answer { get; set; }
        public bool IsRightAnswer { get; set; }
        public virtual ICollection<StudentAnswer> StudentAnswers { get; set; }


    }
}
