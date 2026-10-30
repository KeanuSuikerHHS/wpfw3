using System;
using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Models
{
    public class BlogModel
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "Een titel is verplicht.")]
        [MaxLength(200, ErrorMessage = "De titel mag maximaal 200 tekens bevatten.")]
        public required string Titel {get; set;}

        [Required(ErrorMessage = "Een bericht is verplicht.")]
        public required string Bericht {get; set;}

        [Required(ErrorMessage = "Een datum is verplicht.")]
        public DateOnly Datum {get; set;}
    }
}
