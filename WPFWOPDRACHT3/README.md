# Portfolio Backend API

Dit is de backend voor mijn persoonlijke portfolio, gemaakt voor de module Backend met ASP.NET Core (Opdracht 3).

**Repository:** https://github.com/KeanuSuikerHHS/wpfw3/tree/main/WPFWOPDRACHT3

## Technische specificaties
- **Framework:** .NET 10 (ASP.NET Core Web API)
- **API-stijl:** Minimal API
- **Database:** SQL Server Express
- **ORM:** Entity Framework Core (EF Core)
- **Authenticatie & Autorisatie:** ASP.NET Core Identity met cookie-authenticatie en rolgebaseerde autorisatie
- **Testen:** xUnit (HTTP-integratietests met `HttpClient` en `CookieContainer`)

## Architectuur & Mappenstructuur
Om de code overzichtelijk te houden en te voldoen aan *Separation of Concerns*, heb ik een meerlagenarchitectuur (Layered Architecture) gebruikt. De solution bestaat uit twee projecten:

### `WPFWOPDRACHT3` (API)
- `Endpoints/`: Minimal API routes (`AuthEndpoints`, `projectEndpoints`, `blogEndpoints`) die de HTTP-requests opvangen en afhandelen.
- `Services/`: De daadwerkelijke businesslogica (`DataBaseService`). De endpoints sturen hun acties hiernaartoe door.
- `Data/`: Bevat de databasecontext (`PortofolioContext`, erft van `IdentityDbContext<UserModel>`) voor de koppeling met SQL Server.
- `Models/`: De EF Core entiteiten (`ProjectModel`, `BlogModel`, `UserModel`) die overeenkomen met de tabellen in de database.
- `Dtos/`: Data Transfer Objects (`AuthDTOs`, `ProjectDTOs`, `BlogDTOs`). Deze zorgen ervoor dat de API nooit rechtstreeks de interne databasemodellen naar buiten stuurt of ontvangt.
- `Migrations/`: Door EF Core gegenereerde migraties om de database en Identity-tabellen op te bouwen en bij te werken.

### `WPFWOPDRACHT3.Tests` (Testproject)
- `TestClientHelper.cs`: Helper-class die voor elke test een `HttpClient` met een eigen `CookieContainer` aanmaakt en kan inloggen via `/auth/login`.
- `tests/AuthTests.cs`: Tests voor registreren, inloggen, wachtwoordvalidatie en cookie-uitgifte.
- `tests/ProjectTests.cs`: CRUD- en autorisatietests voor alle `/project`-routes.
- `tests/BlogTests.cs`: CRUD- en autorisatietests voor alle `/blog`-routes.

## Authenticatie & Autorisatie
De API maakt gebruik van **ASP.NET Core Identity** met cookie-authenticatie. Voor API-verzoeken zijn de standaard cookie-redirects uitgeschakeld, zodat ongeautoriseerde verzoeken direct `401 Unauthorized` of `403 Forbidden` teruggeven.

### Rollen
Bij het opstarten van de applicatie worden de volgende rollen automatisch geseed in de database:
- **Gast:** Standaardrol bij registratie via `/auth/register`. Mag alleen publieke `GET`-routes aanroepen.
- **Editor:** Mag naast lezen ook projecten en blogs aanmaken, wijzigen en verwijderen.
- **Admin:** Heeft dezelfde schrijfrechten als een Editor (via de policy `EditorOrAdmin`).

### Endpoints & Beveiliging
| Route | Methode | Toegang |
| :--- | :--- | :--- |
| `/auth/register` | `POST` | Publiek (registreert als `Gast`) |
| `/auth/login` | `POST` | Publiek (geeft auth-cookie terug) |
| `/project` & `/project/{id}` | `GET` | Publiek |
| `/project` & `/project/{id}` | `POST`, `PUT`, `DELETE` | Policy `EditorOrAdmin` |
| `/blog` & `/blog/{id}` | `GET` | Publiek |
| `/blog` & `/blog/{id}` | `POST`, `PUT`, `DELETE` | Policy `EditorOrAdmin` |

## Database instellen
De data wordt opgeslagen in een lokale SQL Server. Controleer de `DefaultConnection` in `appsettings.json` en pas deze zo nodig aan naar jouw eigen SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=localhost\\SQLEXPRESS02;Initial Catalog=PortofolioDB;Integrated Security=True;TrustServerCertificate=True;"
}