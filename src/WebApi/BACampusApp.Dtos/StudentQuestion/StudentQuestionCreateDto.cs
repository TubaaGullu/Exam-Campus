using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.StudentQuestion
{
    public class StudentQuestionCreateDto
    {
        public int QuestionOrder { get; set; }
        public int Score { get; set; }
        public Guid QuestionId { get; set; }
    }
}
