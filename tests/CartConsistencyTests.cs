using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static Microsoft.Playwright.Assertions;
using System.Text.RegularExpressions;
using tests.Pages;
namespace tests;
public class CartConsistencyTests : PageTest{
    [Test]
    public async Task Cart_ShowsSameNameAndPriceAsCatalog(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        var catalogName = await inventory.GetFirstItemName();
        var catalogPrice = await inventory.GetFirstItemPrice();
        await inventory.AddFirstItemToCart();
        var cart = await inventory.OpenCart();
        Assert.That(await cart.GetFirstItemName(), Is.EqualTo(catalogName));
        Assert.That(await cart.GetFirstItemPrice(), Is.EqualTo(catalogPrice));
    }

    [Test]
    public async Task Cart_RemoveItem_EmptiesCart(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);
        await inventory.AddFirstItemToCart();
        var cart = await inventory.OpenCart();
        await cart.RemoveFirstItem();
        Assert.That(await cart.GetItemsCount(), Is.EqualTo(0));
    }

    [Test]
    public async Task Sorting_ZtoA_ShowsLastItemFirst(){
        var login = new LoginPage(Page);
        await login.Open();
        var inventory = await login.LoginAs(TestConfig.StandardUser, TestConfig.Password);

        await inventory.SortBy("Name (Z to A)");

        Assert.That(await inventory.GetFirstItemName(), Is.EqualTo("Test.allTheThings() T-Shirt (Red)"));
    }
}