using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Reposatories._Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Instractor> Instractors { get; set; }
        public DbSet<Stud_Review> Stud_Reviews { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<CourseBooking> CourseBookings { get; set; }
        public DbSet<CourseContent> CourseContents { get; set; }
        public DbSet<Stud_Projects> Stud_Projects { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<WorkshopAudience> WorkshopAudiences { get; set; }
        public DbSet<WorkshopBooking> WorkshopBookings { get; set; }
        public DbSet<Settings> Settings { get; set; }

    }
}
