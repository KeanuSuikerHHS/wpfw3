namespace WPFWOPDRACHT3.Dtos.BlogDTOs
{
    public record GetBlogDTO
    (
        int Id,
        string Titel,
        string Bericht,
        DateOnly Datum
    );
}
