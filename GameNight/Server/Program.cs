using GameNight.Server.Database;
using GameNight.Server.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GameContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("GameContext"))
                   .UseSnakeCaseNamingConvention()
            );

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
            options.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
            options.CallbackPath = "/auth/callback-google";
            options.ReturnUrlParameter = "ReturnUrl";
            //options.SignInScheme = IdentityConstants.ApplicationScheme;
        })
    .AddIdentityCookies();


builder.Services.AddAuthorization();


builder.Services.AddIdentityCore<User>(endpoints =>
{
    endpoints.SignIn.RequireConfirmedEmail = false;
    endpoints.User.RequireUniqueEmail = true;
})
    .AddSignInManager()
    .AddEntityFrameworkStores<GameContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

//app.MapGroup("api/auth")
//   .MapIdentityApi<User>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();