using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IExamRepository : IAsyncFindableRepository<Exam>, IAsyncInsertableRepository<Exam>, IAsyncRepository, IAsyncDeleteableRepository<Exam>, IAsyncUpdateableRepository<Exam>
    {
    }
}
