using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroesServer.Models
{
    public enum Gravite { BAS=0, MOYEN ,ELEVE}
    public enum TypeAlerte { MEDICAL=0, SECURITE }
    public class Alerte
    {
        public int IdAlert { get; set; }
        public int IdInitiateur { get; set; }
        public int IdRepondant { get; set; }
        public Localisation Position { get; set; }
        public Gravite Gravite { get; set; }
        public TypeAlerte Type { get; set; }

    }
}