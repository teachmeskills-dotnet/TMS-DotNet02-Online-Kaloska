using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Constants;
using TMS_DotNet02_Online_Kaloska.TmTracker.Data.Models;

namespace TMS_DotNet02_Online_Kaloska.TmTracker.Data.Configurations
{
    /// <summary>
    /// EF Configuration for Goal entity.
    /// </summary>
    class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Goals,Schema.Tracker)
                .HasKey(goal => goal.Id);

            builder.Property(goal => goal.Id)
                .UseIdentityColumn();

            builder.Property(goal => goal.Text)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);

            builder.Property(goal => goal.Start)
                .HasColumnType(SqlConfiguration.SqlDateTimeFormat);

            builder.Property(goal => goal.End)
                .HasColumnType(SqlConfiguration.SqlDateTimeFormat);

            builder.HasOne(goal => goal.Project)
                .WithMany(project => project.Goals)
                .HasForeignKey(goal => goal.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
