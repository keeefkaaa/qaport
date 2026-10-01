using Microsoft.Playwright;
using System.Threading.Tasks;
namespace tests;
public static class TestHelpers{
    public static async Task LoginAs(IPage page, string username, string password){
        await page.GotoAsync(TestConfig.BaseUrl);
        await page.GetByPlaceholder("Username").FillAsync(username);
        await page.GetByPlaceholder("Password").FillAsync(password);
        await page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
    }
}