using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Dtos.BlogDTOs
{

    public record CreateBlogDTO
    (
        [Required] string Titel,
        [Required] string Bericht,
        DateOnly Datum
    );
}
