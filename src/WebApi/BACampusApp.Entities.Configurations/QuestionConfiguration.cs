using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class QuestionConfiguration : AuditableEntityTypeConfiguration<Question>
    {
        public override void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(x => x.QuestionText).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Image).IsRequired(false);
            builder.HasOne(x => x.QuestionTopic).WithMany(x => x.Questions).HasForeignKey(x => x.QuestionTopicId).OnDelete(DeleteBehavior.Cascade);
            base.Configure(builder);

        }
    }
}
