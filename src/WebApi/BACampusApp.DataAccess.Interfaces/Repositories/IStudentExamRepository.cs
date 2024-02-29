using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IStudentExamRepository : IRepository, IAsyncRepository, IAsyncQueryableRepository<StudentExam>, IAsyncFindableRepository<StudentExam>, IAsyncUpdateableRepository<StudentExam>, IAsyncInsertableRepository<StudentExam>, IAsyncDeleteableRepository<StudentExam>
    {
    }
}
