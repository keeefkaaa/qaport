using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using tests.Pages;
namespace tests;
public class InventoryTests : PageTest{
    [Test]
    public async Task Sorting_ByPriceLowToHigh(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        await inventory.SortBy("Price (low to high)");
        await Expect(inventory.Prices.First).ToHaveTextAsync("$7.99");
    }
    [Test]
    public async Task AddToCart_BadgeUpdates(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        await inventory.AddFirstItemToCart();
        await Expect(inventory.CartButton).ToContainTextAsync("1");
    }
    [Test]
    public async Task Logout_ReturnsToLoginPage(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        await inventory.Logout();
        await Expect(login.UsernameForCheck).ToBeVisibleAsync();
    }
}