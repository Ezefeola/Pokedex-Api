using Domain.Pokemons;
using Domain.Pokemons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class PokemonConfiguration : EntityTypeBaseConfiguration<Pokemon>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                    id => id.Value,
                    value => PokemonId.Create(value)            
               )
               .IsRequired()
               .ValueGeneratedNever();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Pokemon> builder)
    {
        builder.Property(x => x.Number)
               .IsRequired();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(Pokemon.Rules.NAME_MAX_LENGTH)
               .HasColumnName(nameof(Pokemon.Name));

        builder.Property(x => x.Height)
               .IsRequired()
               .HasPrecision(18,2)
               .HasColumnName(nameof(Pokemon.Height));

        builder.Property(x => x.Weight)
               .IsRequired()
               .HasPrecision(18,2)
               .HasColumnName(nameof(Pokemon.Weight));

        builder.Property(x => x.ImageUrl)
               .IsRequired()
               .HasColumnName(nameof(Pokemon.ImageUrl));

        builder.Property(x => x.Type1)
               .IsRequired()
               .HasMaxLength(Pokemon.Rules.TYPE1_MAX_LENGTH)
               .HasColumnName(nameof(Pokemon.Type1));

        builder.Property(x => x.Type2)
               .IsRequired(false)
               .HasMaxLength(Pokemon.Rules.TYPE2_MAX_LENGTH)
               .HasColumnName(nameof(Pokemon.Type2));

        BaseEntityConfig.ApplyTo(builder);
    }
}