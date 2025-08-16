using Domain.Users;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class UserConfiguration : EntityTypeBaseConfiguration<User>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                  id => id.Value,
                  value => UserId.Create(value)
               )
               .HasColumnName(nameof(User.Id))
               .ValueGeneratedNever();

    }

    protected override void ConfigurateProperties(EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(f => f.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(x => x.FirstName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.FIRST_NAME_MAX_LENGTH)
                           .HasColumnName(nameof(User.FullName.FirstName));

            fullNameBuilder.Property(x => x.LastName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.LAST_NAME_MAX_LENGTH)
                           .HasColumnName(nameof(User.FullName.LastName));
        });

        builder.ComplexProperty(x => x.EmailAddress, emailBuilder =>
        {
            emailBuilder.Property(x => x.Value)
                        .IsRequired()
                        .HasMaxLength(User.Rules.EMAIL_MAX_LENGTH)
                        .HasColumnName("Email");

        });

        BaseEntityConfig.ApplyTo<User>(builder);
    }
}