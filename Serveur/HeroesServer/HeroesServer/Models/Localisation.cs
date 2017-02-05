using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroesServer.Models
{
    public class Localisation
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}