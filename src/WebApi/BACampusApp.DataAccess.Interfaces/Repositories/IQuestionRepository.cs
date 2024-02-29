using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IQuestionRepository : IAsyncFindableRepository<Question>, IAsyncInsertableRepository<Question>, IAsyncRepository, IAsyncDeleteableRepository<Question>, IAsyncUpdateableRepository<Question>
    {
    }
}
