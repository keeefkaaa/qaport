using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
namespace tests;
public class InventoryTests : PageTest{
    [Test]
    public async Task Sorting_ByPriceLowToHigh(){
        await TestHelpers.LoginAs(Page, TestConfig.StandardUser, TestConfig.Password);
        await Page.GetByRole(AriaRole.Combobox).SelectOptionAsync(new SelectOptionValue { Label = "Price (low to high)" });
        await Expect(Page.Locator(".inventory_item_price").First).ToHaveTextAsync("$7.99");
    }
    [Test]
    public async Task AddToCart_BadgeUpdates(){
        await TestHelpers.LoginAs(Page, TestConfig.StandardUser, TestConfig.Password);
        await Page.GetByRole(AriaRole.Button,new() { Name = "Add to cart" }).First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Button, new() { NameRegex = new Regex("Cart, 1") })).ToBeVisibleAsync();
    }
    [Test]
    public async Task Logout_ReturnsToLoginPage(){
        await TestHelpers.LoginAs(Page, TestConfig.StandardUser, TestConfig.Password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" }).ClickAsync();
        await Page.Locator("[data-test='logout-sidebar-link']").ClickAsync();
        await Expect(Page.GetByPlaceholder("Username")).ToBeVisibleAsync();
    }
}