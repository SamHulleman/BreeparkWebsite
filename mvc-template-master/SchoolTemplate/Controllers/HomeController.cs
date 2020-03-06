﻿using System;
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


        [Route("festival/{id}")]
        public IActionResult Festival(string id)
        {
            var model = GetFestival(id);
            return View(model);
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
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Datum = DateTimeOffset.Parse(reader["datum"].ToString()),
                            Tijd = reader["tijd"].ToString(),
                            Prijs = reader["prijs"].ToString(),
                            Beschikbaarheid = reader["Beschikbaarheid"].ToString()
                        };
                    
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }

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
                            Beschrijving = reader["beschrijving"].ToString(),
                            Datum = DateTimeOffset.Parse(reader["datum"].ToString()),
                            Tijd = reader["tijd"].ToString(),
                            Prijs = reader["Prijs"].ToString(),
                            Beschikbaarheid = reader["Beschikbaarheid"].ToString()
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals[0];
        }
    }
}
