using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using tests.Pages;
namespace tests;
public class CheckoutTests : PageTest{
    private async Task<CheckoutInfoPage> ReachCheckoutForm(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        await inventory.AddFirstItemToCart();
        var cart = await inventory.OpenCart();
        return await cart.Checkout();
    }
    [Test]
    public async Task Checkout_WithoutFirstName_ShowsError(){
        var info = await ReachCheckoutForm();
        await info.FillForm("", "Tester", "12345");
        await info.Continue();
        await Expect(info.Error).ToHaveTextAsync("Error: First Name is required");
    }
    [Test]
    public async Task Checkout_WithoutLastName_ShowsError(){
        var info = await ReachCheckoutForm();
        await info.FillForm("Elena", "", "12345");
        await info.Continue();
        await Expect(info.Error).ToHaveTextAsync("Error: Last Name is required");
    }
    [Test]
    public async Task Checkout_WithoutZip_ShowsError(){
        var info = await ReachCheckoutForm();
        await info.FillForm("Elena", "Tester", "");
        await info.Continue();
        await Expect(info.Error).ToHaveTextAsync("Error: Postal Code is required");
    }
    [Test]
    public async Task CheckoutOverview_Cancel_ReturnsToInventory(){
        var info = await ReachCheckoutForm();
        await info.FillForm("Elena", "Tester", "12345");
        var overview = await info.Continue();
        await overview.Cancel();
        await Expect(Page).ToHaveURLAsync(new Regex("inventory.html"));
    }
}