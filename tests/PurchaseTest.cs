using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using tests.Pages;
namespace tests;
public class PurchaseTests : PageTest{
    [Test]
    public async Task FullPurchase_CompletesSuccessfully(){
        var loginPage = new LoginPage(Page);
        await loginPage.Open();
        var inventory = await loginPage.LoginAs(TestConfig.StandardUser, TestConfig.Password);

        await inventory.AddFirstItemToCart();
        var cart = await inventory.OpenCart();
        Assert.That(await cart.GetItemsCount(), Is.EqualTo(1));

        var info = await cart.Checkout();
        await info.FillForm("Elena", "Tester", "12345");
        var overview = await info.Continue();

        var subtotal = await overview.GetSubtotal();
        var tax = await overview.GetTax();
        var total = await overview.GetTotal();
        Assert.That(subtotal + tax, Is.EqualTo(total));

        var complete = await overview.Finish();
        await Expect(complete.ThankYouHeader).ToHaveTextAsync("Thank you for your order!");
    }
}