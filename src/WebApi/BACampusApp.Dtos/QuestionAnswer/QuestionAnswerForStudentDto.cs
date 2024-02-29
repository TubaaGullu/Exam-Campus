using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.QuestionAnswer
{
    public class QuestionAnswerForStudentDto
    {
        public Guid QuestionId { get; set; }
        public string Answer { get; set; }
        public Guid Id { get; set; }
    }
}
