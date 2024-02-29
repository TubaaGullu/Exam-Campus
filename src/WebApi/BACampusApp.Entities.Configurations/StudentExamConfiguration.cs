using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class StudentExamConfiguration : AuditableEntityTypeConfiguration<StudentExam>
    {
        public override void Configure(EntityTypeBuilder<StudentExam> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Score).IsRequired(false);
            builder.Property(x => x.IsFinished).HasColumnType("bool").IsRequired();
            builder.HasOne(x => x.Exam).WithMany(x => x.StudentExams).HasForeignKey(x => x.ExamId);
            builder.HasOne(x => x.Student).WithMany(x => x.StudentExams).HasForeignKey(x => x.StudentId);
            builder.HasAlternateKey(x => new { x.ExamId, x.StudentId });
        }
    }
}
