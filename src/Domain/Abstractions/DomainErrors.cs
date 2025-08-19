namespace Domain.Abstractions;
public static class DomainErrors
{
    public static readonly string START_DATE_GREATER_THAN_END_DATE = "Start date must be before end date.";
    public static class Users
    {
        public static readonly string EMAIL_NOT_EMPTY = "The email field cannot be empty.";
        public static readonly string EMAIL_INVALID_FORMAT = "Invalid email format.";

        public static readonly string FIRST_NAME_NOT_EMPTY = "The FirstName field cannot be empty.";
        public static readonly string LAST_NAME_NOT_EMPTY = "The LastNmae field cannot be empty.";

        public static readonly string ONE_TIME_PASSWORD_NOT_EMPTY = "OneTimePassword cannot be empty.";

        public static readonly string NOT_FOUND = "User not found";
    }

    public static class UserPokemons
    {
        public static readonly string USER_ID_NOT_EMPTY = "The user id can not be empty.";
        public static readonly string POKEMON_ID_NOT_EMPTY = "The pokemon id can not be empty.";
    }

    public static class Pokemons
    {
        public static readonly string NAME_NOT_EMPTY = "The name can not be empty";
        public static readonly string NOT_FOUND = "Pokemon not found";
    }
}