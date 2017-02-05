using HeroesServer.DAL;
using HeroesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HeroesServer.Controllers
{
    
    public class AlerteController : ApiController
    {

        [ActionName("FindNewAlert")]
        [HttpGet]
        public Alerte FindNewAlert(float longitude, float latitude, int UID)
        {
            UserData.UpdatePositionUser(UID, longitude, latitude);
            List<Alerte> alertes = AlerteData.GetAllAlerteEnCours();
            Localisation loc = new Localisation();
            loc.Longitude = longitude;
            loc.Latitude = latitude;
            int range = 500;
            foreach (Alerte alerte in alertes)
            {
                var dist = CalculDistances(alerte.Position, loc);
                if (CalculDistances(alerte.Position, loc) < range)
                {
                    return alerte;
                }
            }
            return null;
        }


        [ActionName("PushNewAlert")]
        [HttpGet]
        public bool PushNewAlert(int type, int gravite, float longitude, float latitude, int idInitiateur)
        {
            Alerte alerte = new Alerte();
            alerte.IdInitiateur = idInitiateur;
            alerte.Gravite = (Gravite)gravite;
            alerte.Type = (TypeAlerte)type;
            Localisation loc = new Localisation();
            loc.Longitude = longitude;
            loc.Latitude = latitude;
            alerte.Position = loc;
            AlerteData.Insert(alerte);
            return true;
        }

        [ActionName("AcceptAlert")]
        [HttpGet]
        public Alerte AcceptAlert(int idRepondant, int idInitiateur) //enlever le parametre en question AID?
        {
            AlerteData.Update(idRepondant, idInitiateur);
            return new Alerte();
            //la mettre statique?
        }

        [ActionName("IsAlertAnswered")]
        [HttpGet]
        public bool IsAlertAnswered(int idInitiateur)
        {
            Alerte AlerteTemp = AlerteData.GetByIDInitiateur(idInitiateur);
            return true;//AlerteTemp.IsAnswered;
        }

        [ActionName("GetHelperDistance")]
        [HttpGet]
        public float GetHelperDistance( int  UID)
        {
            Alerte AlerteTemp = AlerteData.GetByIDInitiateur(UID);
            User Helper = UserData.Get(AlerteTemp.IdRepondant);

            float distance = CalculDistances(AlerteTemp.Position, Helper.Position);

            return (float)distance;
        }
        public static float CalculDistances(Localisation position1,Localisation position2)
        {
            var R = 6371; // km
            var dLat = ConvertDegreesToRadians(position2.Latitude - position1.Latitude);

            var dLon = ConvertDegreesToRadians(position2.Longitude - position1.Longitude);
            var lat1 = ConvertDegreesToRadians(position1.Latitude);
            var lat2 = ConvertDegreesToRadians(position2.Latitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;

            return (float)d;
        }
        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}
