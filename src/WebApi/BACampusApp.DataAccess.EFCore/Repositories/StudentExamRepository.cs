using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class StudentExamRepository : EFBaseRepository<StudentExam>, IStudentExamRepository
    {
        public StudentExamRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
