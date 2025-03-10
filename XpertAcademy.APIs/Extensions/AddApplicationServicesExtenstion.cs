using Microsoft.EntityFrameworkCore;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Services.Identity;
using XpertAcademy.Reposatories._Data;
using XpertAcademy.Reposatories.Repositories;
using XpertAcademy.Service.Services;
using XpertAcademy.Service.Services.Identity;

namespace XpertAcademy.APIs.Extensions
{
    public static class AddApplicationServicesExtenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddTransient<IUnitOfWork, UnitOfWork>();
            Services.AddTransient<ICourseService, CourseService>();
            Services.AddTransient<IFileUploadService, FileUploadService>();
            Services.AddTransient<IInstractorService, InstractorService>();
            Services.AddTransient<IStud_ReviewService, Stud_ReviewService>();
            Services.AddTransient<IContactUsService, ContactUsService>();
            Services.AddTransient<IBookingService, BookingService>();
            Services.AddTransient<IStud_ProjectService, Stud_ProjectService>();
            Services.AddTransient<ICompanyService, CompanyService>();
            Services.AddTransient<IWorkshopService, WorkshopService>();
            Services.AddTransient<ISettingsService, SettingsService>();

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddScoped<ITokenService, TokenService>();

            return Services;
        }
    }
}
