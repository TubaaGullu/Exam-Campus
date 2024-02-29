using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.DbSets
{
    public class Question : AuditableEntity
    {
        public Question()
        {
            StudentQuestions = new HashSet<StudentQuestion>();
            QuestionAnswers = new HashSet<QuestionAnswer>();

        }

        public string QuestionText { get; set; } //soru metni
        public string? Image { get; set; }
        public virtual IEnumerable<QuestionAnswer> QuestionAnswers { get; set; }

        public virtual QuestionTopic QuestionTopic { get; set; }

        public Guid QuestionTopicId { get; set; }

        public virtual ICollection<StudentQuestion> StudentQuestions { get; set; }

    }
}
