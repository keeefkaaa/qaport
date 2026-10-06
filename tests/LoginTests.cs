using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using tests.Pages;
namespace tests;
public class LoginTests : PageTest{
    [Test]
    public async Task StandardUser_CanLogin(){
        var login = new LoginPage(Page);
        await login.Open();
        await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        await Expect(Page).ToHaveURLAsync(new Regex(".*inventory"));
    }
    [Test]
    public async Task LockedOutUser_CannotLogin(){
        var login = new LoginPage(Page);
        await login.Open();
        await login.LoginAs(TestConfig.LockedUser, TestConfig.Password);
        await Expect(login.Error).ToContainTextAsync("locked out");
    }
    [Test]
    public async Task WrongPassword_ShowsError(){
        var login = new LoginPage(Page);
        await login.Open();
        await login.LoginAs(TestConfig.StandardUser, "wrong_password");
        await Expect(login.Error).ToContainTextAsync("do not match");
    }
}