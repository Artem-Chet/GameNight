namespace GameNight.Shared.Auth;


public static class AuthDefaults
{
    public const string UnAuthorizedClientName = "GameNight.ServerAPI";
    public const string AuthorizedClientName = "GameNight.ServerAPI.Authorized";

    public const string LogInPath = "auth/signin-google";
    public const string LogOutPath = "auth/logout";
}
