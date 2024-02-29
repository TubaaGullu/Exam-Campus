﻿using BACampusApp.Dtos.StudentExam;
using BACampusApp.Dtos.StudentQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Dtos.Exam
{
    public class ExamCreateDto
    {
        public string Name { get; set; }
        public DateTime ExamDateTime { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string? Description { get; set; }
        public bool? IsStarted { get; set; } = false;
        public int MaxScore { get; set; }
        public ICollection<StudentExamCreateDto> StudentExams { get; set; } = new List<StudentExamCreateDto>();
        public ICollection<StudentQuestionCreateDto> StudentQuestions { get; set; } = new List<StudentQuestionCreateDto>();


    }
}