using System;

namespace SchoolTemplate.Database
{
    public class FestivalDag
    {
        public int Id { get; set; }

        public int Festival_id { get; set; }

        public DateTimeOffset Datum { get; set; }
        public string Start { get; set; }

        public string Eind { get; set; }

        public int Voorraad { get; set; }

    }
}

