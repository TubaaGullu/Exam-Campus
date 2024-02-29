using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class ExamRepository :EFBaseRepository<Exam>, IExamRepository
    {
        public ExamRepository(BACampusAppDbContext context) : base(context)
        {

        }
    }
}
