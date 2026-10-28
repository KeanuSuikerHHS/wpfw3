using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos
{

    public record BlogDTO
    (
        [Required] int Id,
        [Required] string Titel,
        [Required] string Bericht,
        [Required] DateOnly Datum
    );
}
