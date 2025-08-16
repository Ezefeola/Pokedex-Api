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
               .HasColumnName(User.ColumnNames.Id)
               .ValueGeneratedNever();

    }

    protected override void ConfigurateProperties(EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(f => f.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(x => x.FirstName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.FIRST_NAME_MAX_LENGTH)
                           .HasColumnName(User.ColumnNames.FirstName);

            fullNameBuilder.Property(x => x.LastName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.LAST_NAME_MAX_LENGTH)
                           .HasColumnName(User.ColumnNames.LastName);
        });

        builder.ComplexProperty(x => x.EmailAddress, emailBuilder =>
        {
            emailBuilder.Property(x => x.Value)
                        .IsRequired()
                        .HasMaxLength(User.Rules.EMAIL_MAX_LENGTH)
                        .HasColumnName(User.ColumnNames.Email);

        });
        builder.HasIndex(x => x.EmailAddress.Value)
               .IsUnique();

        BaseEntityConfig.ApplyTo<User>(builder);
    }
}