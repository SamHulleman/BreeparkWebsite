using System;

namespace SchoolTemplate.Database
{
    public class Festival
    {
        public int id { get; set; }

        public string naam { get; set; }

        public string beschrijving { get; set; }

        public DateTimeOffset datum { get; set; }
        public string tijd { get; set; }

        public string prijs { get; set; }
    }
}

