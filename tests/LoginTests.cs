using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
namespace tests;
public class LoginTests : PageTest{
    [Test]
    public async Task StandardUser_CanLogin(){
        await TestHelpers.LoginAs(Page, TestConfig.StandardUser, TestConfig.Password);
        await Expect(Page).ToHaveURLAsync(new Regex(".*inventory"));
    }

    [Test]
    public async Task LockedOutUser_CannotLogin(){
        await TestHelpers.LoginAs(Page, TestConfig.LockedUser, TestConfig.Password);
        await Expect(Page.Locator("[data-test='error']")).ToContainTextAsync("locked out");
    }
    [Test]
    public async Task WrongPassword_ShowsError(){
        await TestHelpers.LoginAs(Page, TestConfig.StandardUser, "wrong_password");
        await Expect(Page.Locator("[data-test='error']")).ToContainTextAsync("do not match");
    }
}