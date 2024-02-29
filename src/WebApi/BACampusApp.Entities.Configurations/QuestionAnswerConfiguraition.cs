using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class QuestionAnswerConfiguraition: AuditableEntityTypeConfiguration<QuestionAnswer>
    {
        public override void Configure(EntityTypeBuilder<QuestionAnswer> builder)
        {
            builder.Property(x => x.Answer).IsRequired();
            builder.Property(x => x.IsRightAnswer).IsRequired();
            builder.HasOne(x => x.Question).WithMany(x => x.QuestionAnswers).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
