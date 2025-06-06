﻿using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Auth0UserLogin.Models;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Auth0UserLogin.Controllers;

public class AuthController : Controller
{
    //redirecting the user to Auth0's login page.
    public async Task Login(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();
        await HttpContext.ChallengeAsync(
            Auth0Constants.AuthenticationScheme,
            authenticationProperties
        );
    }

    //Displays the authenticated user's profile information on the /user/profile page.
    [Authorize]
    [Route("/user/profile")]
    public IActionResult Profile()
    {
        return View(new UserProfileViewModel
        {
            Name = User.Identity?.Name ?? string.Empty,
            EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
            ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? string.Empty
        });
    }

    //Logs the user out from both the application and Auth0.
    [Authorize]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, new AuthenticationProperties
        {
            RedirectUri = Url.Action("Index", "Home")
        });

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}