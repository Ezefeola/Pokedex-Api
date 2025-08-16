namespace Application.Utilities.Validations;
public class ValidationMessages
{
    public static string NOT_EMPTY = "The {PropertyName} field cannot be empty.";
    public static string MAX_LENGTH = "The {PropertyName} field cannot exceed {MaxLength} characters.";
    public static string MIN_LENGTH = "The {PropertyName} field cannot be less than {MinLength} characters.";
    public static string GREATER_THAN_ZERO = "The {PropertyName} field must be greater than 0.";
    public static string INVALID_EMAIL = "The {PropertyName} field must be a valid email address.";

    public static class Auth
    {
        public static string INVALID_CREDENTIALS = "Invalid credentials.";
        public static string INVALID_TOKEN = "Invalid token.";
        public static string TOKEN_EXPIRED = "Token has expired.";
    }

    public static class User
    {
        public static string USER_EMAIL_EXISTS = "A user with this email already exists.";
        public static string INVALID_ROLE = "The {PropertyName} field must be a valid user role.";
    }
}