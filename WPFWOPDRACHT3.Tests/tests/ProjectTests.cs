using System.Net;
using System.Net.Http.Json;
using WPFWOPDRACHT3.Models;
using Xunit;

namespace WPFWOPDRACHT3.Tests;

public class ProjectTests
{
    private const string Password = "Password123!";

    [Fact]
    public async Task GetAll_Publiek_GeeftOkEnLijst()
    {
        var client = TestClientHelper.CreateClient();

        var response = await client.GetAsync("/project");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var projecten = await response.Content.ReadFromJsonAsync<List<ProjectModel>>();
        Assert.NotNull(projecten);
    }

    [Theory]
    [InlineData(99999)]
    [InlineData(-1)]
    public async Task GetById_OnbekendId_GeeftNotFound(int id)
    {
        var client = TestClientHelper.CreateClient();

        var response = await client.GetAsync($"/project/{id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_ZonderLogin_GeeftUnauthorized()
    {
        var client = TestClientHelper.CreateClient();
        var payload = new
        {
            Titel = "Ongeautoriseerd",
            Beschrijving = "Test",
            Categorie = "Web",
            GitHubURL = "https://github.com/test",
            Datum = "2026-11-25"
        };

        var response = await client.PostAsJsonAsync("/project", payload);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_AlsGast_GeeftForbidden()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("gast@test.nl", Password);
        var payload = new
        {
            Titel = "Gast Project",
            Beschrijving = "Test",
            Categorie = "Web",
            GitHubURL = "https://github.com/test",
            Datum = "2026-11-25"
        };

        var response = await client.PostAsJsonAsync("/project", payload);

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Theory]
    [InlineData("editor@test.nl", "Editor")]
    [InlineData("admin@test.nl", "Admin")]
    public async Task Post_AlsEditorOfAdmin_MaaktProjectAan(string email, string role)
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync(email, Password);
        var payload = new
        {
            Titel = $"Project van {role}",
            Beschrijving = "Beschrijving",
            Categorie = "API",
            GitHubURL = "https://github.com/test/repo",
            Datum = "2026-11-25"
        };

        var response = await client.PostAsJsonAsync("/project", payload);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<ProjectModel>();
        Assert.NotNull(created);
        Assert.Equal($"Project van {role}", created.Titel);

        var getResponse = await TestClientHelper.CreateClient().GetAsync($"/project/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var fetched = await getResponse.Content.ReadFromJsonAsync<ProjectModel>();
        Assert.Equal(created.Titel, fetched?.Titel);
    }

    [Fact]
    public async Task Post_OngeldigeInvoer_GeeftBadRequest()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("editor@test.nl", Password);
        var payload = new { };

        var response = await client.PostAsJsonAsync("/project", payload);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_AlsEditor_WijzigtProjectInDatabase()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("editor@test.nl", Password);
        var createResponse = await client.PostAsJsonAsync("/project", new
        {
            Titel = "Oude Titel",
            Beschrijving = "Oud",
            Categorie = "Web",
            GitHubURL = "https://github.com/oud",
            Datum = "2026-11-25"
        });
        var created = await createResponse.Content.ReadFromJsonAsync<ProjectModel>();

        var updateResponse = await client.PutAsJsonAsync($"/project/{created!.Id}", new
        {
            Titel = "Nieuwe Titel",
            Beschrijving = "Nieuw",
            Categorie = "Backend",
            GitHubURL = "https://github.com/nieuw",
            Datum = "2026-11-25"
        });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var getResponse = await TestClientHelper.CreateClient().GetAsync($"/project/{created.Id}");
        var updated = await getResponse.Content.ReadFromJsonAsync<ProjectModel>();
        Assert.Equal("Nieuwe Titel", updated?.Titel);
    }

    [Fact]
    public async Task Delete_AlsAdmin_VerwijdertProjectUitDatabase()
    {
        var client = await TestClientHelper.CreateLoggedInClientAsync("admin@test.nl", Password);
        var createResponse = await client.PostAsJsonAsync("/project", new
        {
            Titel = "Te Verwijderen",
            Beschrijving = "Test",
            Categorie = "Web",
            GitHubURL = "https://github.com/del",
            Datum = "2026-11-25"
        });
        var created = await createResponse.Content.ReadFromJsonAsync<ProjectModel>();

        var deleteResponse = await client.DeleteAsync($"/project/{created!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        var getResponse = await TestClientHelper.CreateClient().GetAsync($"/project/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}