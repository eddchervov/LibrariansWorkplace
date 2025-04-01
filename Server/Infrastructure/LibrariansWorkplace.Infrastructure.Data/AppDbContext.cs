using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using LibrariansWorkplace.Domain;

namespace LibrariansWorkplace.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{

#nullable disable
    public DbSet<Book> Books { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<IssuedBook> IssuedBooks { get; set; }
#nullable restore

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        //Database.EnsureCreated();
    }

    public void BeginTransaction()
    {
        Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Database.CommitTransaction();
    }

    public void RollbackTransaction()
    {
        Database.RollbackTransaction();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //warning!!! with this approach you have no control over the order in which configurations are applied!!!
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
