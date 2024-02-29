using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentAnswer
{
    public class StudentAnswerCreateDto
    {
        public bool IsSelected { get; set; }
        public Guid QuestionAnswerId { get; set; }
        public Guid StudentQuestionId { get; set; }
    }
}
