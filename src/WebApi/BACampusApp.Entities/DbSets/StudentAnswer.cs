using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class StudentAnswer :AuditableEntity
    {
        public bool IsSelected { get; set; }

        //Navigation Prop.
        public Guid QuestionAnswerId { get; set; }
        public virtual QuestionAnswer? QuestionAnswer { get; set; }
        public Guid StudentQuestionId { get; set; }
    }
}
