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
    public class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.Property(c => c.Type)
              .HasConversion(
              (rev) => rev.ToString(),
              (type) => (WorkshopType)Enum.Parse(typeof(WorkshopType), type, true));
        }
    }
}
