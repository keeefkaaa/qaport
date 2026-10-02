using Microsoft.Playwright;
namespace tests.Pages;
public class CheckoutOverviewPage{
    private readonly IPage _page;
    public CheckoutOverviewPage(IPage page) => _page = page;
    private ILocator ItemTotal => _page.Locator("[data-test='subtotal-label']");
    private ILocator Tax => _page.Locator("[data-test='tax-label']");
    private ILocator Total => _page.Locator("[data-test='total-label']");
    private ILocator FinishButton => _page.Locator("[data-test='finish']");
    private ILocator CancelButton => _page.Locator("[data-test='cancel']");
    public async Task<decimal> GetSubtotal() => await ParsePrice(ItemTotal);
    public async Task<decimal> GetTax() => await ParsePrice(Tax);
    public async Task<decimal> GetTotal() => await ParsePrice(Total);
    private static async Task<decimal> ParsePrice(ILocator locator){
        var text = await locator.TextContentAsync() ?? "";
        var number = text.Split('$')[1];
        return decimal.Parse(number, System.Globalization.CultureInfo.InvariantCulture);
    }

    public async Task<CheckoutCompletePage> Finish(){
        await FinishButton.ClickAsync();
        return new CheckoutCompletePage(_page);
    }

    public async Task<InventoryPage> Cancel(){
        await CancelButton.ClickAsync();
        return new InventoryPage(_page);
    }
}