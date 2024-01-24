using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace GameNight.Server.Auth;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    public AuthController(SignInManager<User> signInManager, IUserStore<User> userStore, UserManager<User> userManager)
    {
        SignInManager = signInManager;
        UserStore = (IUserEmailStore<User>)userStore; // This is so ugly but IUserEmailStore is not defined in di
        UserManager = userManager;
    }

    private SignInManager<User> SignInManager { get; }
    private IUserEmailStore<User> UserStore { get; }
    private UserManager<User> UserManager { get; }

    [HttpPost("signin-google")]
    [HttpGet("signin-google")]
    public ChallengeHttpResult SignUpWithGoogle([FromQuery] string? returnUrl)
    {
        var provider = "Google";

        if (string.IsNullOrEmpty(returnUrl))
        {
            returnUrl = "/";
        }

        IEnumerable<KeyValuePair<string, StringValues>> query = [
            new("returnUrl", returnUrl),
        ];

        var redirectUrl = UriHelper.BuildRelative(
                    Request.PathBase,
                    "/auth/after-callback-google",
                    QueryString.Create(query)
                    );

        var properties = SignInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return TypedResults.Challenge(properties, [provider]);
    }


    [HttpPost("after-callback-google")]
    [HttpGet("after-callback-google")]
    public async Task<ActionResult<AuthenticationToken>> CallBackFromGoogle([FromQuery] string returnUrl)
    {
        var externalLoginInfo = await SignInManager.GetExternalLoginInfoAsync();

        if (externalLoginInfo is null)
        {
            // TODO: Add better error redirect
            return Redirect("/");
        }

        var result = await SignInManager.ExternalLoginSignInAsync(
            externalLoginInfo.LoginProvider,
            externalLoginInfo.ProviderKey,
            isPersistent: false,
            bypassTwoFactor: true);

        // Check if account already exists
        if (result.Succeeded)
        {
            return Redirect(returnUrl);
        }


        // Create new account 

        var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email) ?? throw new Exception("Email claim not provided from google");

        var user = new User();        

        await UserStore.SetUserNameAsync(user, email, default);
        await UserStore.SetEmailAsync(user, email, default);
        await UserStore.SetEmailConfirmedAsync(user, true, default); // We trust google to have confirmed the email

        var createResult = await UserStore.CreateAsync(user, default);

        if (createResult.Succeeded)
        {
            var saveExternalLoginResult = await UserManager.AddLoginAsync(user, externalLoginInfo);
        }

        return Redirect(returnUrl);
    }

    [HttpGet("logout")]
    public SignOutResult Logout()
    {
       return SignOut(new AuthenticationProperties { RedirectUri = "/"}, IdentityConstants.ApplicationScheme);
    }
}
