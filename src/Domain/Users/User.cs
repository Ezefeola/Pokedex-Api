using Domain.Abstractions;
using Domain.Common.DomainResults;
using Domain.Common.ValueObjects;
using Domain.Users.ValueObjects;

namespace Domain.Users;
public sealed class User : Entity<UserId>
{
    public static class Rules
    {
        public const int FIRST_NAME_MAX_LENGTH = 100;
        public const int FIRST_NAME_MIN_LENGTH = 1;

        public const int LAST_NAME_MAX_LENGTH = 100;
        public const int LAST_NAME_MIN_LENGTH = 1;

        public const int EMAIL_MAX_LENGTH = 255;
        public const int EMAIL_MIN_LENGTH = 2;

        public const int PASSWORD_MAX_LENGTH = 250;
        public const int PASSWORD_MIN_LENGTH = 8;
    }
    public static class AuthorizationPolicies
    {
        public const string RequireAdmin = "RequireAdmin";
        public const string RequireManager = "RequireManager";
        public const string RequireCollaborator = "RequireCollaborator";
    }
    public enum UserRolesEnum
    {
        Admin = 1,
        Manager = 2,
        Collaborator = 3
    }

    public FullName FullName { get; private set; } = default!;
    public EmailAddress EmailAddress { get; private set; } = default!;
    public string Password { get; set; } = default!;
    public UserRolesEnum Role { get; set; } = default!;

    private User() { }

    public static DomainResult<User> Create(
        string firstName,
        string lastName,
        string email,
        string otpHash,
        UserRolesEnum role
    )
    {
        List<string> errors = [];

        DomainResult<FullName> fullNameResult = FullName.Create(firstName, lastName);
        if (!fullNameResult.IsSuccess) errors.AddRange(fullNameResult.Errors);

        DomainResult<EmailAddress> emailAddressResult = EmailAddress.Create(email);
        if (!emailAddressResult.IsSuccess) errors.AddRange(emailAddressResult.Errors);

        if(errors.Count > 0)
        {
            return DomainResult<User>.Failure(errors);
        }

        User user = new()
        {
            Id = UserId.NewId(),
            EmailAddress = emailAddressResult.Value,
            FullName = fullNameResult.Value,
            Role = role
        };

        return DomainResult<User>.Success(user)
                                 .WithDescription("User created successfully.");
    }

    public DomainResult<User> Update(
       string? firstName,
       string? lastName,
       string? email,
       UserRolesEnum? role
   )
    {
        List<string> errors = [];
        int totalUpdatedCount = 0;

        DomainResult<User> fullNameResult = UpdateFullName(firstName, lastName);
        if (!fullNameResult.IsSuccess) errors.AddRange(fullNameResult.Errors);
        totalUpdatedCount += fullNameResult.UpdatedFieldCount;

        DomainResult<User> emailResult = UpdateEmail(email);
        if (!emailResult.IsSuccess) errors.AddRange(emailResult.Errors);
        totalUpdatedCount += emailResult.UpdatedFieldCount;

        DomainResult<User> roleIdResult = UpdateRole(role);
        if (!roleIdResult.IsSuccess) errors.AddRange(roleIdResult.Errors);
        totalUpdatedCount += roleIdResult.UpdatedFieldCount;

        if (errors.Count > 0)
        {
            return DomainResult<User>.Failure(errors);
        }

        string descriptionMessage = totalUpdatedCount > 0
                                    ? $"User updated successfully. {totalUpdatedCount}"
                                    : "No changes were made.";

        return DomainResult<User>.Success(this)
                                 .WithDescription(descriptionMessage);
    }

    public DomainResult<User> UpdateFullName(string? firstName, string? lastName)
    {
        DomainResult<FullName> fullNameUpdateResult = FullName.UpdateIfChanged(firstName, lastName);
        if (!fullNameUpdateResult.IsSuccess)
        {
            return DomainResult<User>.Failure(fullNameUpdateResult.Errors);
        }

        FullName = fullNameUpdateResult.Value;
        return DomainResult<User>.Success(this)
                                 .WithUpdatedFieldCount(fullNameUpdateResult.UpdatedFieldCount);
    }

    public DomainResult<User> UpdateEmail(string? email)
    {
        DomainResult<EmailAddress> emailUpdateResult = EmailAddress.UpdateIfChanged(email);
        if (!emailUpdateResult.IsSuccess)
        {
            return DomainResult<User>.Failure(emailUpdateResult.Errors);
        }

        EmailAddress = emailUpdateResult.Value;
        return DomainResult<User>.Success(this)
                                 .WithUpdatedFieldCount(emailUpdateResult.UpdatedFieldCount);
    }

    public DomainResult<User> UpdateRole(UserRolesEnum? role)
    {
        if (Role == role)
        {
            return DomainResult<User>.Success(this)
                                     .WithDescription("No changes made to RoleId.")
                                     .WithUpdatedFieldCount(0);
        }

        if (role is null)
        {
            return DomainResult<User>.Success(this)
                                     .WithDescription("Role null, no changes made.");
        }

        Role = role.Value;
        return DomainResult<User>.Success(this)
                                 .WithUpdatedFieldCount(1);
    }
}