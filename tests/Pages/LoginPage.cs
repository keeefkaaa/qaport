using Microsoft.Playwright;
namespace tests.Pages;
public class LoginPage{
    private readonly IPage _page;
    public LoginPage(IPage page) => _page = page;
    private ILocator Username => _page.GetByPlaceholder("Username");
    private ILocator Password => _page.GetByPlaceholder("Password");
    private ILocator LoginButton => _page.GetByRole(AriaRole.Button, new() { Name = "Login" });
    public ILocator Error => _page.Locator("[data-test='error']");
    public ILocator UsernameForCheck => Username;
    public async Task Open() => await _page.GotoAsync(TestConfig.BaseUrl);
    public async Task<InventoryPage> LoginAs(string user, string pass){
        await Username.FillAsync(user);
        await Password.FillAsync(pass);
        await LoginButton.ClickAsync();
        return new InventoryPage(_page);
    }
    public async Task<string> GetErrorText() => await Error.TextContentAsync() ?? "";
}