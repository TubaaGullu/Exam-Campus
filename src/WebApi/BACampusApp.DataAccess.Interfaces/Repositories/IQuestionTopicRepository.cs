using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IQuestionTopicRepository : IAsyncFindableRepository<QuestionTopic>, IAsyncInsertableRepository<QuestionTopic>, IAsyncRepository, IAsyncDeleteableRepository<QuestionTopic>, IAsyncUpdateableRepository<QuestionTopic>
    {
    }
}
