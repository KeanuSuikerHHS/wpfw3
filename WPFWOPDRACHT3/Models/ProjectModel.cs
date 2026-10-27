using System;

namespace WPFWOPDRACHT3.Models
{
    public class ProjectModel
    {
        public int Id {get; set;}

        public string Titel {get; set;}

        public string Beschrijving {get; set;}

        public string Categorie {get; set;}

        public string GitHubURL {get; set;}

        public DateOnly Datum {get; set;}
    }
}
