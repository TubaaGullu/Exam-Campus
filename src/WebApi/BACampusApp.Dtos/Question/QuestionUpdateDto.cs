using BACampusApp.Dtos.QuestionAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Question
{
    public class QuestionUpdateDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string? Image { get; set; }
        public IEnumerable<QuestionAnswerDto> QuestionAnswers { get; set; }
        public Guid QuestionTopicId { get; set; }

    }
}
