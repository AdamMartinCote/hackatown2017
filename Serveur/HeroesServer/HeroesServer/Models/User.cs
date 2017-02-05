using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroesServer.Models;

namespace HeroesServer.Models
{
    public enum NiveauCompetence { FAIBLE, MOYEN, EXPERT };

    public class User
    {
        public int idUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public NiveauCompetence NiveauCompetence { get; set; }
        public Localisation Position { get; set; }
        //a voir plus tard
        








    }
}