using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;

namespace tests;

public class VariousTests : PageTest{
    private async Task LoginAs(string username, string password){
        await Page.GotoAsync("https://www.saucedemo.com");
        await Page.GetByPlaceholder("Username").FillAsync(username);
        await Page.GetByPlaceholder("Password").FillAsync(password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
    }

    [Test]
    public async Task LockedOutUser_CannotLogin(){
        await LoginAs("locked_out_user", "secret_sauce");
        await Expect(Page.Locator("[data-test='error']")).ToContainTextAsync("locked out");
    }

    [Test]
    public async Task WrongPassword_ShowsError(){
        await LoginAs("standard_user", "wrong_password");
        await Expect(Page.Locator("[data-test='error']")).ToContainTextAsync("do not match");
    }

    [Test]
    public async Task Sorting_ByPriceLowToHigh(){
        await LoginAs("standard_user", "secret_sauce");
        await Page.GetByRole(AriaRole.Combobox).SelectOptionAsync(new SelectOptionValue { Label = "Price (low to high)" });
        await Expect(Page.Locator(".inventory_item_price").First).ToHaveTextAsync("$7.99");
    }

    [Test]
    public async Task AddToCart_BadgeUpdates(){
        await LoginAs("standard_user", "secret_sauce");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).First.ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Button,new() { NameRegex = new Regex("Cart, 1") })).ToBeVisibleAsync();
    }

    [Test]
    public async Task Logout_ReturnsToLoginPage(){
        await LoginAs("standard_user", "secret_sauce");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" }).ClickAsync();
        await Page.Locator("[data-test='logout-sidebar-link']").ClickAsync();
        await Expect(Page.GetByPlaceholder("Username")).ToBeVisibleAsync();
    }
}