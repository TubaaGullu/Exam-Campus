using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class ExamConfiguration : AuditableEntityTypeConfiguration<Exam>
    {
        public override void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ExamDateTime).HasColumnType("datetime2").IsRequired();
            builder.Property(x => x.ExamDuration).HasColumnType("time(7)").IsRequired();
            builder.Property(x => x.IsStarted).IsRequired();
            builder.Property(x=>x.MaxScore).IsRequired();
            base.Configure(builder);
        }
    }
}
