# Portfolio Backend API

Dit is de backend voor mijn persoonlijke portfolio, gemaakt voor de module Backend met ASP.NET Core (Opdracht 3).

**Repository:** https://github.com/KeanuSuikerHHS/wpfw3/tree/main/WPFWOPDRACHT3

## Technische specificaties
- **Framework:** .NET 10 (ASP.NET Core Web API)
- **API-stijl:** Minimal API
- **Database:** SQL Server Express
- **ORM:** Entity Framework Core (EF Core)

## Architectuur & Mappenstructuur
Om de code overzichtelijk te houden en te voldoen aan *Separation of Concerns*, heb ik een meerlagenarchitectuur (Layered Architecture) gebruikt. De mappen zijn ingedeeld op technische verantwoordelijkheid:
- `Endpoints/`: Hierin staan de Minimal API routes die de HTTP-requests opvangen en afhandelen.
- `Services/`: Hier zit de daadwerkelijke businesslogica. De endpoints sturen hun acties hiernaartoe door.
- `Data/`: Bevat de databasecontext (`PortofolioContext`) voor de koppeling met SQL Server.
- `Models/`: De EF Core entiteiten (zoals Project en Blogpost) die overeenkomen met de tabellen in de database.
- `Dtos/`: Data Transfer Objects. Deze zorgen ervoor dat de API nooit rechtstreeks de interne databasemodellen naar buiten stuurt of direct ontvangt.
- `Migrations/`: Bevat de door EF Core gegenereerde bestanden om de database op te bouwen en bij te werken.

## Database instellen
De data wordt opgeslagen in een lokale SQL Server. Wil je het project lokaal draaien? Check dan even de `DefaultConnection` in de `appsettings.json` en pas deze zo nodig aan naar jouw eigen SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=LAPTOP-KBDP7051\\SQLEXPRESS02;Initial Catalog=PortofolioDB;Integrated Security=True;TrustServerCertificate=True;"
}