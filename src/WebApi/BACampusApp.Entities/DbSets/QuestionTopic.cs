using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class QuestionTopic : AuditableEntity
    {
        public QuestionTopic()
        {
            Questions = new HashSet<Question>();
        }
        public string TopicName { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
