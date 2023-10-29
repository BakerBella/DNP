using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Assignment_1;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
            });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapControllerRoute(
                name: "login",
                pattern: "account/login",
                defaults: new { controller = "Account", action = "Login" }
            );
            endpoints.MapControllerRoute(
                name: "accessdenied",
                pattern: "account/accessdenied",
                defaults: new { controller = "Account", view = "ActionDenied" }
            );
        });
    }
}
