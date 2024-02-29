using BACampusApp.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.Interfaces.Repositories
{
    public interface IQuestionAnswerRepository :IAsyncFindableRepository<QuestionAnswer>, IAsyncInsertableRepository<QuestionAnswer>, IAsyncRepository, IAsyncDeleteableRepository<QuestionAnswer>, IAsyncUpdateableRepository<QuestionAnswer>
    {
 
    }
}
