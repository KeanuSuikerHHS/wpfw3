using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos
{
    public record ProjectDTO
    (
        [Required] int Id,
        [Required] string Titel,
        [Required] string Beschrijving,
        string Categorie,
        [Required] string GitHubURL,
        DateOnly Datum
    );
}
