using Microsoft.Playwright;
using NUnit.Framework;
namespace tests;
public class ApiTests{
    private IPlaywright _playwright = null!;
    private IAPIRequestContext _context = null!;
    private string _token = null!;
    [SetUp]
    public async Task Setup(){
        _playwright = await Playwright.CreateAsync();
        _context = await _playwright.APIRequest.NewContextAsync(new(){ BaseURL = TestConfig.ApiBaseUrl});
        var auth = await _context.PostAsync("/auth", new(){ DataObject = new { username = "admin", password = "password123" }});
        Assert.That(auth.Status, Is.EqualTo(200));
        var authJson = await auth.JsonAsync();
        _token = authJson.Value.GetProperty("token").GetString()!;
        Assert.That(_token, Is.Not.Empty);
    }
    [TearDown]
    public async Task Teardown(){
        await _context.DisposeAsync();
        _playwright.Dispose();
    }
    private async Task<IAPIResponse> CreateBooking(){
        var response = await _context.PostAsync("/booking", new(){ DataObject = new { firstname = "Elena", lastname = "Tester", totalprice = 111,
        depositpaid = true,bookingdates = new { checkin = "2026-01-01", checkout = "2026-01-10" }, additionalneeds = "Breakfast"}});
        Assert.That(response.Status, Is.EqualTo(200));
        return response;
    }
    [Test]
    public async Task CreateBooking_ReturnsBookingData(){
        var response = await CreateBooking();
        var json = await response.JsonAsync();
        Assert.That(json.Value.GetProperty("bookingid").GetInt32(), Is.GreaterThan(0));
        Assert.That(json.Value.GetProperty("booking").GetProperty("firstname").GetString(), Is.EqualTo("Elena"));
    }
    [Test]
    public async Task GetBooking_ReturnsCreatedData(){
        var createResponse = await CreateBooking();
        var createJson = await createResponse.JsonAsync();
        var id = createJson.Value.GetProperty("bookingid").GetInt32();
        var response = await _context.GetAsync($"/booking/{id}");
        Assert.That(response.Status, Is.EqualTo(200));
        var json = await response.JsonAsync();
        Assert.That(json.Value.GetProperty("lastname").GetString(), Is.EqualTo("Tester"));
        Assert.That(json.Value.GetProperty("totalprice").GetInt32(), Is.EqualTo(111));
    }
    [Test]
    public async Task DeleteBooking_RemovesIt(){
        var createResponse = await CreateBooking();
        var createJson = await createResponse.JsonAsync();
        var id = createJson.Value.GetProperty("bookingid").GetInt32();
        var delete = await _context.DeleteAsync($"/booking/{id}", new(){ Headers = new Dictionary<string, string> { ["Cookie"] = $"token={_token}" }});
        Assert.That(delete.Status, Is.EqualTo(201));
        var get = await _context.GetAsync($"/booking/{id}");
        Assert.That(get.Status, Is.EqualTo(404));
    }
}