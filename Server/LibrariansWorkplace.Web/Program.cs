using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Infrastructure.BL;
using LibrariansWorkplace.Infrastructure.BL.Books;
using LibrariansWorkplace.Infrastructure.BL.Readers;
using LibrariansWorkplace.Infrastructure.Data;
using LibrariansWorkplace.Services.Interfaces.Books;
using LibrariansWorkplace.Services.Interfaces.Readers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace LibrariansWorkplace.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(connectionString, npgsqlOptionsAction: option =>
                 {
                     option.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                 }));

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibrariansWorkplace.Web", Version = "v1" });
            });


            builder.Services.AddSingleton<ISystemClock, SystemClock>();

            builder.Services
                //.AddTransient<IBookRepository, BookRepository>()
                .AddTransient<IBookService, BookService>()
                .AddTransient<IReaderService, ReaderService>()
                .AddTransient<IBookManagementService, BookManagementService>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                ;

            var app = builder.Build();

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibrariansWorkplace.Web v1"));

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
