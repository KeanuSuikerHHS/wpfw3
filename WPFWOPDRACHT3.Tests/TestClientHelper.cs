using System.Net;
using System.Net.Http.Json;

namespace WPFWOPDRACHT3.Tests;

public static class TestClientHelper
{
    public const string BaseUrl = "http://localhost:5141";

    public static HttpClient CreateClient()
    {
        var handler = new HttpClientHandler
        {
            CookieContainer = new CookieContainer(),
            UseCookies = true
        };

        return new HttpClient(handler)
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }

    public static async Task<HttpClient> CreateLoggedInClientAsync(string username, string password)
    {
        var client = CreateClient();
        var response = await client.PostAsJsonAsync("/auth/login", new
        {
            Username = username,
            Password = password
        });
        response.EnsureSuccessStatusCode();
        return client;
    }
}