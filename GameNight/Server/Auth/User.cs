using Microsoft.AspNetCore.Identity;

namespace GameNight.Server.Auth;

public class User : IdentityUser<Guid>
{
    public User()
    {
        SecurityStamp = Guid.NewGuid().ToString();
    }
}
