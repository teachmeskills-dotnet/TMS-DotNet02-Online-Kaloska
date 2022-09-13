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
    /// EF Configuration for Record entity.
    /// </summary>
    class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Records, Schema.Tracker)
                .HasKey(record => record.Id);

            builder.Property(record => record.Id)
                .UseIdentityColumn();

            builder.Property(record => record.Start)
                .HasColumnType(SqlConfiguration.SqlDateTimeFormat);

            builder.Property(record => record.End)
                .HasColumnType(SqlConfiguration.SqlDateTimeFormat);

            builder.HasOne(record => record.Project)
                .WithMany(project => project.Records)
                .HasForeignKey(record => record.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(record => record.User)
                .WithMany(user => user.Records)
                .HasForeignKey(record => record.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
