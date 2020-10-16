using System;

// haal juiste data op van database voor een specifiek festival
namespace SchoolTemplate.Database
{
    public class Festival
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Beschrijving { get; set; }

        public DateTimeOffset Datum { get; set; }
        public string Tijd { get; set; }

        public string Prijs { get; set; }

        public string Afbeelding { get; set; }
    }
}

