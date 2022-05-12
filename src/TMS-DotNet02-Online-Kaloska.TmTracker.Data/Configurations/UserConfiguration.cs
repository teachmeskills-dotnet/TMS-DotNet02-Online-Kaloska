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
    /// EF Configuration for User entity.
    /// </summary>
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Users, Schema.User)
                .HasKey(user => user.Id);

            builder.Property(user => user.FullName)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);
        }
    }
}
