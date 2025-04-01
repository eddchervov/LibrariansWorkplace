using LibrariansWorkplace.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrariansWorkplace.Infrastructure.Data.EntityConfigurations;

internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books").HasKey(x => x.Id);

        builder.HasIndex(x => x.Name);
        // other configs
    }
}
