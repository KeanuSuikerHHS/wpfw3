using System.Net;
using System.Net.Http.Json;
using WPFWOPDRACHT3.Models;
using Xunit;

namespace WPFWOPDRACHT3.Tests;

public class BlogTests
{
    private const string Password = "Password123!";

    [Fact]
    public async Task GetAll_Publiek_GeeftOkEnLijst()
    {
        var client = TestClientHelper.CreateClient();

        var response = await client.GetAsync("/blog");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var blogs = await response.Content.ReadFromJsonAsync<List<BlogModel>>();
        Assert.NotNull(blogs);
    }

    [Theory]
    [InlineData(99999)]
    [InlineData(-1)]
    public async Task GetById_OnbekendId_GeeftNotFound(int id)
    {
        var client = TestClientHelper.CreateClient();

        var response = await client.GetAsync($"/blog/{id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_ZonderLogin_GeeftUnauthorized()
    {
        var client = TestClientHelper.CreateClient();
        var payload = new
        {
            Titel = "Ongeautoriseerd",
            Bericht = "Testbericht",
            Datum = "2026-11-25"
        };

        var response = await client.PostAsJsonAsync("/blog", payload);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_AlsGast_GeeftForbidden()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("gast@test.nl", Password);
        var payload = new
        {
            Titel = "Gast Blog",
            Bericht = "Testbericht",
            Datum = "2026-11-25"
        };

        var response = await client.PostAsJsonAsync("/blog", payload);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Theory]
    [InlineData("editor@test.nl", "Editor")]
    [InlineData("admin@test.nl", "Admin")]
    public async Task Post_AlsEditorOfAdmin_MaaktBlogAan(string email, string role)
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync(email, Password);
        var payload = new
        {
            Titel = $"Blog van {role}",
            Bericht = "Inhoud van de blogpost",
            Datum = "2026-11-25"
        };

        var response = await client.PostAsJsonAsync("/blog", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<BlogModel>();
        Assert.NotNull(created);
        Assert.Equal($"Blog van {role}", created.Titel);

        var getResponse = await TestClientHelper.CreateClient().GetAsync($"/blog/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetched = await getResponse.Content.ReadFromJsonAsync<BlogModel>();
        Assert.Equal(created.Titel, fetched?.Titel);
    }

    [Fact]
    public async Task Post_OngeldigeInvoer_GeeftBadRequest()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("editor@test.nl", Password);
        var payload = new { };

        var response = await client.PostAsJsonAsync("/blog", payload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_AlsEditor_WijzigtBlogInDatabase()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("editor@test.nl", Password);
        var createResponse = await client.PostAsJsonAsync("/blog", new
        {
            Titel = "Oude Blog",
            Bericht = "Oude tekst",
            Datum = "2026-11-25"
        });
        var created = await createResponse.Content.ReadFromJsonAsync<BlogModel>();

        var updateResponse = await client.PutAsJsonAsync($"/blog/{created!.Id}", new
        {
            Titel = "Nieuwe Blog",
            Bericht = "Nieuwe tekst",
            Datum = "2026-11-25"
        });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var getResponse = await TestClientHelper.CreateClient().GetAsync($"/blog/{created.Id}");
        var updated = await getResponse.Content.ReadFromJsonAsync<BlogModel>();
        Assert.Equal("Nieuwe Blog", updated?.Titel);
    }

    [Fact]
    public async Task Delete_AlsAdmin_VerwijdertBlogUitDatabase()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("admin@test.nl", Password);
        var createResponse = await client.PostAsJsonAsync("/blog", new
        {
            Titel = "Te Verwijderen Blog",
            Bericht = "Weg ermee",
            Datum = "2026-11-25"
        });
        var created = await createResponse.Content.ReadFromJsonAsync<BlogModel>();

        var deleteResponse = await client.DeleteAsync($"/blog/{created!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        var getResponse = await TestClientHelper.CreateClient().GetAsync($"/blog/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}