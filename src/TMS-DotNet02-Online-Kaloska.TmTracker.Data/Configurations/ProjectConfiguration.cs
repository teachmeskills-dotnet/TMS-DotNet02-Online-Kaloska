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
    /// EF Configuration for Project entity.
    /// </summary>
    class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Projects, Schema.Tracker)
                .HasKey(project => project.Id);

            builder.Property(project => project.Id)
                .UseIdentityColumn();

            builder.Property(project => project.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(project => project.CreationTime)
                .HasColumnType(SqlConfiguration.SqlDateTimeFormat);
        }
    }
}
