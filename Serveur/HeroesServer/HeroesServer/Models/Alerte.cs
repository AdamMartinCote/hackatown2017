using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroesServer.Models
{
    public enum Gravite { BAS, ELEVE}
    public class Alerte
    {
        public ApplicationUser Initiateur { get; set; }
        public ApplicationUser Repondant { get; set; }
        public Localisation Position { get; set; }
        public Gravite Gravite { get; set; }

    }
}