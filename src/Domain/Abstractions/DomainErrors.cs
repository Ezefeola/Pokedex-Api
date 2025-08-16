namespace Domain.Abstractions;
public static class DomainErrors
{
    public static readonly string START_DATE_GREATER_THAN_END_DATE = "Start date must be before end date.";
    public static class UserErrors
    {
        public static readonly string EMAIL_NOT_EMPTY = "The email field cannot be empty.";
        public static readonly string EMAIL_INVALID_FORMAT = "Invalid email format.";

        public static readonly string FIRST_NAME_NOT_EMPTY = "The FirstName field cannot be empty.";
        public static readonly string LAST_NAME_NOT_EMPTY = "The LastNmae field cannot be empty.";

        public static readonly string ONE_TIME_PASSWORD_NOT_EMPTY = "OneTimePassword cannot be empty.";
    }
}