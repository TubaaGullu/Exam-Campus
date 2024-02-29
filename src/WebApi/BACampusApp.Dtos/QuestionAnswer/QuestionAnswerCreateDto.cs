using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.QuestionAnswer
{
    public class QuestionAnswerCreateDto
    {
        public string Answer { get; set; }
        public bool IsRightAnswer { get; set; }
    }
}
