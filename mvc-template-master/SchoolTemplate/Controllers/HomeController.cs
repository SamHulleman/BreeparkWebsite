using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {
        //database connectie
        string connectionString = "Server=172.16.160.21;Port=3306;Database=109807;Uid=109807;Pwd=rfultyRa;";
        // string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=109807;Uid=109807;Pwd=rfultyRa;";
         

        //homescreen: dit zorgt ervoor dat je begint op de homescreen
        public IActionResult Index()
        {
            return View(GetFestivals());
           
        }
        //redirect naar de id paginas van de festivals
        [Route("festival/{Id}")]
        public IActionResult Festival(string id)
        {
            var model = GetFestival(id);
            var Festivaldagen = GetFestivalDag(id);
            ViewData["festivaldagen"] = Festivaldagen;

            return View(model);
        }
        //redirect naar gelukt
        [Route("gelukt")]
        public IActionResult Gelukt()
        {
            return View();
        }
        //redirect naar Route
        [Route("Route")]
        public IActionResult Route()
        {
            return View();

        }
        //redirect naar contact
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();

        }
        //als het contactformulier goed is ingevuld volgens de requirements dan word je naar gelukt gestuurd
        [Route("Contact")]
        [HttpPost]
        public IActionResult Contact(PersonModel model)
       {
            if (!ModelState.IsValid)
                return View(model);

            SavePerson(model);

            return Redirect("/gelukt");
        }
        //data naar database sturen
        private void SavePerson(PersonModel person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam,achternaam,emailadres,geboortedatum) VALUEs(?voornaam,?achternaam,?emailadres,?geboortedatum)", conn);
                cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
                cmd.Parameters.Add("?emailadres", MySqlDbType.VarChar).Value = person.Email;
                cmd.Parameters.Add("?geboortedatum", MySqlDbType.Date).Value = person.Geboortedatum;
                cmd.ExecuteNonQuery();
            }
        }

        [Route("Agenda")]
        public IActionResult Agenda()
        {
            return View(GetFestivals());
        }

        //errors
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }    
        //een lijst van de festivals die er zijn die in agenda worden getoond uit de database
        private List<Festival> GetFestivals()
        {

            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival f = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Prijs = reader["Prijs"].ToString(),
                            Afbeelding = reader["Afbeelding"].ToString(),

                        };
                    
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }

        //zet data neer van de database voor een festival
        private Festival GetFestival(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival f = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Afbeelding = reader["Afbeelding"].ToString(),
                            Prijs = reader["Prijs"].ToString(),
                            
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals[0];
        }
        //selecteer alles van de database festival_dag waar de festival_id is geselecteerd
        private List<FestivalDag> GetFestivalDag(string festivalId)
        {
            List<FestivalDag> festivals = new List<FestivalDag>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival_dag where festival_id = {festivalId}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FestivalDag f = new FestivalDag
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Festival_id = Convert.ToInt32(reader["Festival_id"]),
                            Datum = DateTime.Parse(reader["Datum"].ToString()),
                            Start = reader["Start"].ToString(),
                            Eind = reader["Eind"].ToString(),
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }
    }
      
    }

