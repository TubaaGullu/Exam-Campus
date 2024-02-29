using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Entities.Configurations
{
    public class QuestionTopicConfiguration :AuditableEntityTypeConfiguration<QuestionTopic>
    {
        public override void Configure(EntityTypeBuilder<QuestionTopic> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.TopicName).IsRequired().HasMaxLength(256);

        }
    }
}
