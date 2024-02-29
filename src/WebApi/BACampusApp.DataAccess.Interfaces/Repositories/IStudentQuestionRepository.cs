using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IStudentQuestionRepository : IAsyncRepository, IAsyncQueryableRepository<StudentQuestion>, IAsyncFindableRepository<StudentQuestion>, IAsyncUpdateableRepository<StudentQuestion>, IAsyncInsertableRepository<StudentQuestion>, IAsyncDeleteableRepository<StudentQuestion>
    {
    }
}
