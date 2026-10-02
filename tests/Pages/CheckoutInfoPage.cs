using Microsoft.Playwright;
namespace tests.Pages;
public class CheckoutInfoPage{
    private readonly IPage _page;
    public CheckoutInfoPage(IPage page) => _page = page;
    private ILocator FirstName => _page.GetByPlaceholder("First Name");
    private ILocator LastName => _page.GetByPlaceholder("Last Name");
    private ILocator ZipCode => _page.GetByPlaceholder("Zip/Postal Code");
    private ILocator ContinueButton => _page.Locator("[data-test='continue']");
    private ILocator CancelButton => _page.Locator("[data-test='cancel']");
    public ILocator Error => _page.Locator("[data-test='error']");
    public async Task FillForm(string first, string last, string zip){
        await FirstName.FillAsync(first);
        await LastName.FillAsync(last);
        await ZipCode.FillAsync(zip);
    }
    public async Task<CheckoutOverviewPage> Continue(){
        await ContinueButton.ClickAsync();
        return new CheckoutOverviewPage(_page);
    }
    public async Task<CartPage> Cancel(){
        await CancelButton.ClickAsync();
        return new CartPage(_page);
    }
    public async Task<string> GetErrorText() => await Error.TextContentAsync() ?? "";
}