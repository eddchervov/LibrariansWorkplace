using LibrariansWorkplace.Domain;
using LibrariansWorkplace.Domain.Interfaces;
using LibrariansWorkplace.Domain.Interfaces.IssuedBooks;
using LibrariansWorkplace.Infrastructure.BL;
using LibrariansWorkplace.Infrastructure.BL.Books;
using LibrariansWorkplace.Infrastructure.BL.Readers;
using LibrariansWorkplace.Infrastructure.Data;
using LibrariansWorkplace.Infrastructure.Data.Repositories;
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
                 })
                 .UseSeeding((context, _) =>
                 {
                     if (context.Set<Book>().Any() == false)
                     {
                         context.Set<Book>().Add(new Book { Author = "Александр Куприн", Name = "Гранатовый браслет", CountCopies = 20, YearPublication = 1990 });
                         context.Set<Book>().Add(new Book { Author = "Федор Достоевский", Name = "Бедные люди", CountCopies = 20, YearPublication = 1982 });
                         context.Set<Book>().Add(new Book { Author = "Владимир Даль", Name = "Русские сказки", CountCopies = 20, YearPublication = 1976 });
                         context.Set<Book>().Add(new Book { Author = "Александр Пушкин", Name = "Руслан и Людмила", CountCopies = 20, YearPublication = 1967 });
                     }

                     if (context.Set<Book>().Any() == false)
                     {
                         context.Set<Reader>().Add(new Reader { FullName = "Иванов Иван Иванович", DateBirth = new DateTime(1990, 1, 16).ToUniversalTime() });
                         context.Set<Reader>().Add(new Reader { FullName = "Петрова Мария Александровна", DateBirth = new DateTime(1987, 4, 4).ToUniversalTime() });
                         context.Set<Reader>().Add(new Reader { FullName = "Сидров Игорь Валерьевич", DateBirth = new DateTime(1992, 10, 10).ToUniversalTime() });
                     }

                     context.SaveChanges();
                 }));

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibrariansWorkplace.Web", Version = "v1" });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowAnyHeader());
            });

            builder.Services.AddSingleton<ISystemClock, SystemClock>();

            builder.Services
                .AddTransient<IBookService, BookService>()
                .AddTransient<IReaderService, ReaderService>()
                .AddTransient<IBookManagementService, BookManagementService>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IIssuedBooksRepository, IssuedBooksRepository>()
                ;

            builder.WebHost.UseUrls("http://*:5005");

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibrariansWorkplace.Web v1"));

            app.UseCors("CorsPolicy");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
}
