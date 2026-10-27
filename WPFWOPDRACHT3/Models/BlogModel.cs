using System;

namespace WPFWOPDRACHT3.Models
{
    public class BlogModel
    {
        public int Id {get; set;}

        public string Titel {get; set;}

        public string Bericht {get; set;}

        public DateOnly Datum {get; set;}
    }
}
