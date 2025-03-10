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
    public class ReviewesConfiguration : IEntityTypeConfiguration<Stud_Review>
    {
        public void Configure(EntityTypeBuilder<Stud_Review> builder)
        {
            builder.Property(c => c.ReviewType)
              .HasConversion(
              (rev) => rev.ToString(),
              (type) => (ReviewType)Enum.Parse(typeof(ReviewType), type, true));
        }
    }
}
