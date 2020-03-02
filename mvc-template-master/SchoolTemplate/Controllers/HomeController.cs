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
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=172.16.160.21;Port=3306;Database=109807;Uid=109807;Pwd=rfultyRa;";


        public IActionResult Index()
        {     

            return View(GetFestivals());
        }      

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("Agenda")]
        public IActionResult Agenda()
        {
            return View(GetFestivals());
        }
        [Route("Tickets")]
        public IActionResult Tickets()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }    

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
                            id = Convert.ToInt32(reader["Id"]),
                            naam = reader["Naam"].ToString(),
                            beschrijving = reader["beschrijving"].ToString(),
                            datum = reader["datum"].ToString(),
                            tijd = reader["tijd"].ToString(),
                            prijs = reader["prijs"].ToString()
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }
    }
}
