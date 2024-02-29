using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class QuestionAnswerRepository :EFBaseRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(BACampusAppDbContext context) : base(context)
        {
            
        }


    }
}
