using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos.ProjectDTOs
{
    public record CreateProjectDTO
    (
        [Required] string Titel,
        [Required] string Beschrijving,
        string? Categorie,
        [Required] string GitHubURL,
        DateOnly Datum
    );
}
