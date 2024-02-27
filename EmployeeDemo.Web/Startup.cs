using EmployeeDemo.Domain.Employee;
using EmployeeDemo.EF;
using EmployeeDemo.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDemo.Web
{
    public class Startup
    {
        public IConfiguration configuration { get; }
        private readonly IWebHostEnvironment env1;

        public Startup(IConfiguration _configuration,IWebHostEnvironment _env)
        {
            configuration = _configuration;
            env1 = _env;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllersWithViews();
            services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<EmployeesService>();
            services.AddScoped<SkillsService>();
            services.AddTransient<IEmployeesRepository, EmployeesRepository>();
            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<ISkillsRepository, SkillsRepository>();
            services.AddTransient<ISkillsService, SkillsService>();
        }

        public void Configure(WebApplication app,IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
