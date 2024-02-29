using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class StudentAnswerConfiguration : AuditableEntityTypeConfiguration<StudentAnswer>
    {
        public override void Configure(EntityTypeBuilder<StudentAnswer> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.IsSelected).IsRequired();
            builder.HasOne(x => x.QuestionAnswer).WithMany(x => x.StudentAnswers).HasForeignKey(x => x.QuestionAnswerId);

            builder.HasAlternateKey(x => new {  x.QuestionAnswerId });
        }
    }
}
