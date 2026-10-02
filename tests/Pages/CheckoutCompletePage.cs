using Microsoft.Playwright;
namespace tests.Pages;
public class CheckoutCompletePage{
    private readonly IPage _page;
    public CheckoutCompletePage(IPage page) => _page = page;
    public ILocator ThankYouHeader => _page.Locator("[data-test='complete-header']");
    private ILocator BackHomeButton => _page.Locator("[data-test='back-to-products']");
    public async Task<string> GetConfirmationText() => await ThankYouHeader.TextContentAsync() ?? "";
    public async Task<InventoryPage> BackToProducts(){
        await BackHomeButton.ClickAsync();
        return new InventoryPage(_page);
    }
}