﻿using BACampusApp.Dtos.QuestionAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Question
{
    public class QuestionAndAnswerListDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string QuestionText { get; set; }
        public string? Image { get; set; }
        public List<QuestionAnswerDto> QuestionAnswers { get; set; }
        public Guid QuestionTopicId { get; set; }

        public string TopicName { get; set; }
    }
}
