using LibrariansWorkplace.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrariansWorkplace.Infrastructure.Data.EntityConfigurations;

internal class ReaderEntityTypeConfiguration : IEntityTypeConfiguration<Reader>
{
    public void Configure(EntityTypeBuilder<Reader> builder)
    {
        builder.ToTable("Readers").HasKey(x => x.Id);

        builder.HasIndex(x => x.FullName);

        // other configs
    }
}