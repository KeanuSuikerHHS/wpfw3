using System;
using System.ComponentModel.DataAnnotations;

namespace WPFWOPDRACHT3.Models
{
    public class ProjectModel
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Een titel is verplicht.")]
        [MaxLength(200, ErrorMessage = "De titel mag maximaal 200 tekens bevatten.")]
        public string Titel {get; set;}

        [Required(ErrorMessage = "Een beschrijving is verplicht.")]

        public string Beschrijving {get; set;}


        public string Categorie {get; set;}

        [Required(ErrorMessage = "Een GitHub URL is verplicht.")]

        public string GitHubURL {get; set;}

        [Required(ErrorMessage = "Een datum is verplicht.")]

        public DateOnly Datum {get; set;}
    }
}
