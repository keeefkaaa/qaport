using Microsoft.Playwright;
using System.Text.RegularExpressions;
namespace tests.Pages;
public class InventoryPage{
    private readonly IPage _page;
    public InventoryPage(IPage page) => _page = page;
    private ILocator SortCombobox => _page.GetByRole(AriaRole.Combobox);
    private ILocator AddToCartButtons => _page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" });
    public ILocator Prices => _page.Locator(".inventory_item_price");
    public ILocator ItemNames => _page.Locator("[data-test='inventory-item-name']");
    private ILocator MenuButton => _page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" });
    private ILocator LogoutLink => _page.Locator("[data-test='logout-sidebar-link']");
    public ILocator CartButton => _page.GetByRole(AriaRole.Button,new() { NameRegex = new Regex("Cart") });
    public async Task SortBy(string label) => await SortCombobox.SelectOptionAsync(new SelectOptionValue { Label = label });
    public async Task AddFirstItemToCart() => await AddToCartButtons.First.ClickAsync();
    public async Task<string> GetFirstItemName() => await ItemNames.First.TextContentAsync() ?? "";
    public async Task<string> GetFirstItemPrice() => await Prices.First.TextContentAsync() ?? "";

    public async Task Logout(){
        await MenuButton.ClickAsync();
        await LogoutLink.ClickAsync();
    }
    public async Task<CartPage> OpenCart(){
        await CartButton.ClickAsync();
        return new CartPage(_page);
    }
}