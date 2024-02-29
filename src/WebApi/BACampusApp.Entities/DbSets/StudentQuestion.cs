using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class StudentQuestion:AuditableEntity
    {


        public int Score { get; set; }
        public int QuestionOrder { get; set; }

        //Navigation Prop.
        public Guid StudentExamId { get; set; }
        public virtual StudentExam? StudentExam { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question? Question { get; set; }

    }
}
