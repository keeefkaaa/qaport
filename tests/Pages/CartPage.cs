using Microsoft.Playwright;
namespace tests.Pages;
public class CartPage{
    private readonly IPage _page;
    public CartPage(IPage page) => _page = page;
    public ILocator CartItems => _page.Locator(".cart_item");
    private ILocator ItemNames => _page.Locator("[data-test='inventory-item-name']");
    private ILocator ItemPrices => _page.Locator("[data-test='inventory-item-price']");
    private ILocator RemoveButtons => _page.GetByRole(AriaRole.Button, new() { Name = "Remove" });
    private ILocator ContinueShoppingButton => _page.Locator("[data-test='continue-shopping']");
    private ILocator CheckoutButton => _page.Locator("[data-test='checkout']");
    public async Task<int> GetItemsCount() => await CartItems.CountAsync();
    public async Task<string> GetFirstItemName() => await ItemNames.First.TextContentAsync() ?? "";

    public async Task<string> GetFirstItemPrice() => await ItemPrices.First.TextContentAsync() ?? "";

    public async Task RemoveFirstItem() => await RemoveButtons.First.ClickAsync();

    public async Task<InventoryPage> ContinueShopping(){
        await ContinueShoppingButton.ClickAsync();
        return new InventoryPage(_page);
    }
    public async Task<CheckoutInfoPage> Checkout(){
        await CheckoutButton.ClickAsync();
        return new CheckoutInfoPage(_page);
    }
}