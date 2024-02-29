using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class StudentQuestionConfiguration : AuditableEntityTypeConfiguration<StudentQuestion>
    {
        public override void Configure(EntityTypeBuilder<StudentQuestion> builder)
        {
            base.Configure(builder);


            builder.Property(x => x.Score).IsRequired();
            builder.Property(x => x.QuestionOrder).IsRequired();
            builder.HasOne(x => x.StudentExam).WithMany(x => x.StudentQuestions).HasForeignKey(x => x.StudentExamId);
            builder.HasOne(x => x.Question).WithMany(x => x.StudentQuestions).HasForeignKey(x => x.QuestionId);

            builder.HasAlternateKey(x => new { x.StudentExamId, x.QuestionId });
        }
    }
}
