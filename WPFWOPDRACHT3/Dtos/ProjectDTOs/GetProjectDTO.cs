namespace WPFWOPDRACHT3.Dtos.ProjectDTOs
{
    public record GetProjectDTO
    (
        int Id,
        string Titel,
        string Beschrijving,
        string Categorie,
        string GitHubURL,
        DateOnly Datum
    );
}
