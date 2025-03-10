using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Enums;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Reposatories._Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(c => c.TitleAR).IsRequired();

            builder.Property(c => c.CourseStatus)
              .HasConversion(
              (course) => course.ToString(),
              (status) => (CourseStatus)Enum.Parse(typeof(CourseStatus), status, true));

        }
    }
}
