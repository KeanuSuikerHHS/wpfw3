using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace WPFWOPDRACHT3.Tests;

public class AuthTests
{
    private const string Password = "Password123!";

    [Fact]
    public async Task Register_GeldigeGebruiker_GeeftOk()
    {
        var client = TestClientHelper.CreateClient();
        var uniqueId = Guid.NewGuid().ToString("N");
        var payload = new
        {
            Email = $"nieuw_{uniqueId}@test.nl",
            Username = $"nieuw_{uniqueId}",
            Password = Password
        };

        var response = await client.PostAsJsonAsync("/auth/register", payload);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Register_ZwakWachtwoord_GeeftBadRequest()
    {
        var client = TestClientHelper.CreateClient();
        var uniqueId = Guid.NewGuid().ToString("N");
        var payload = new
        {
            Email = $"zwak_{uniqueId}@test.nl",
            Username = $"zwak_{uniqueId}",
            Password = "123"
        };

        var response = await client.PostAsJsonAsync("/auth/register", payload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_GeldigeGegevens_GeeftOkEnAuthCookie()
    {
        var client = TestClientHelper.CreateClient();
        var payload = new
        {
            Username = "gast@test.nl",
            Password = Password
        };

        var response = await client.PostAsJsonAsync("/auth/login", payload);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(response.Headers.Contains("Set-Cookie"));
    }

    [Fact]
    public async Task Login_VerkeerdWachtwoord_GeeftUnauthorized()
    {
        var client = TestClientHelper.CreateClient();
        var payload = new
        {
            Username = "gast@test.nl",
            Password = "VerkeerdWachtwoord1!"
        };

        var response = await client.PostAsJsonAsync("/auth/login", payload);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}