using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.DataAccess.EFCore.Repositories
{
    public class QuestionRepository : EFBaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(BACampusAppDbContext context) : base(context)
        {
            
        }
    }
}
