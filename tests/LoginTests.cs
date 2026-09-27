using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;

namespace tests;

public class LoginTests : PageTest
{
    [Test]
    public async Task StandardUser_CanLogin(){
        await Page.GotoAsync("https://www.saucedemo.com/");
        await Page.GetByPlaceholder("Username").FillAsync("standard_user");
        await Page.GetByPlaceholder("Password").FillAsync("secret_sauce");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();

        await Expect(Page).ToHaveURLAsync(new Regex("inventory"));
        await Expect(Page.GetByText("Sauce Labs Backpack")).ToBeVisibleAsync();
    }
}