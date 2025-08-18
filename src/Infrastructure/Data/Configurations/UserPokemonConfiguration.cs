using Domain.Pokemons.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class UserPokemonConfiguration : EntityTypeBaseConfiguration<UserPokemon>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<UserPokemon> builder)
    {
        builder.HasKey(x => new { x.UserId, x.PokemonId });

        builder.HasOne(x => x.User)
               .WithMany(x => x.UserPokemons)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Pokemon)
               .WithMany(x => x.UserPokemons)
               .HasForeignKey(x => x.PokemonId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<UserPokemon> builder)
    {
        builder.Property(x => x.IsCaught)
               .IsRequired();

        BaseEntityConfig.ApplyTo(builder);
    }
}