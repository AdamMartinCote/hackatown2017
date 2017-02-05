using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeroesServer.Models;

namespace HeroesServer.Models
{
    public enum NiveauCompetence { FAIBLE, MOYEN, EXPERT };

    public class Users
    {
        public int idUser { get; set; }
        public string userNom { get; set; }
        public string UserPrenom { get; set; }
        public NiveauCompetence competence { get; set; }
        public Localisation position { get; set; }
        //a voir plus tard
        








    }
}