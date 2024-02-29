﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.QuestionAnswer
{
    public class QuestionAnswerUpdateDto
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsRightAnswer { get; set; }
        public Guid QuestionId { get; set; }
    }
}
