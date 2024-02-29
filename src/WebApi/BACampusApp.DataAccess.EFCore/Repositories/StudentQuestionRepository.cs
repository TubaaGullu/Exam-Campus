﻿using BACampusApp.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class StudentQuestionRepository : EFBaseRepository<StudentQuestion>, IStudentQuestionRepository
    {
        public StudentQuestionRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }

}
