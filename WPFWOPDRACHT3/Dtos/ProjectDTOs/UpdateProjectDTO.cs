using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos.ProjectDTOs
{
    public record UpdateProjectDTO
    (
        [Required] int Id, 
        [Required] string Titel,
        [Required] string Beschrijving,
        string? Categorie,
        [Required] string GitHubURL,
        DateOnly Datum
    );
}
