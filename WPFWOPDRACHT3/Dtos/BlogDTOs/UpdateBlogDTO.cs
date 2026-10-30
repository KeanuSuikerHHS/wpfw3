using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos.BlogDTOs


{
    public record UpdateBlogDTO
    (
        [Required] int Id,
        [Required] string Titel,
        [Required] string Bericht,
        DateOnly Datum
    );
}
