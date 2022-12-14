using Bira.App.Providers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bira.App.Providers.Infra.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1 => Providers : Address
            builder.HasOne(p => p.Address)
                .WithOne(a => a.Provider);

            // 1 : N => Providers : Product
            builder.HasMany(p => p.Products)
                .WithOne(prod => prod.Provider)
                .HasForeignKey(prod => prod.ProviderId);

            builder.ToTable("Fornecedores");

        }
    }
}
