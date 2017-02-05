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

        [HttpGet]
        public User GetUser(int id)
        {
            FindNewAlert(232.0f, 123.0f, 1);
            User user = UserData.Get(id);

            return user;
        }

        /*
        {
            IdInitiateur:"12312",
            IdRepondant: null,
            Position:{
                        Latitude:1213.232,
                        Longitude:32323.232,
                        LastUpdate:2017-02-04 12:00:00
                     }
            Gravite: 0,
            Type: 1
        }
            */
        [HttpPost]
        public void CreateAlert([FromBody] Alerte alerte)
        {
            AlerteData.Insert(alerte);
            
        }

        [HttpPost]
        public Alerte FindNewAlert(float longitude,float latitude,int UID)
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

        [HttpPost]
        public Alerte AcceptAlert(int AID, int HID) //enlever le parametre en question AID?
        {
            User Helper = GetUser(HID);
            Alerte AlerteAConfirmer = FindNewAlert(Helper.Position.Longitude, Helper.Position.Latitude, HID);
            AlerteAConfirmer.IsAnswered = true;
            AlerteAConfirmer.IdRepondant = HID;
            return AlerteAConfirmer;
            //la mettre statique?
        }

        [HttpPost]
        public Alerte PushNewAlert(TypeAlerte type, Gravite gravite, int UID, float longitude, float latitude)
        {
            Alerte NewAlerte =new Alerte();
            NewAlerte.Type = type;
            NewAlerte.Gravite = gravite;
            NewAlerte.IdInitiateur = UID;
            NewAlerte.Position.Longitude = longitude;
            NewAlerte.Position.Latitude = latitude;
            //possibilite d entrer dans un database en soit demander a Chris
            return null;
        }

        [HttpPost]
        public bool IsAlertAnswered(int UID)
        {
            Alerte AlerteTemp = AlerteData.GetByIDInitiator(UID);
            return AlerteTemp.IsAnswered;
        }


        [HttpPost]
        public float GetHelperDistance( int  UID)
        {
            Alerte AlerteTemp = AlerteData.GetByIDInitiator(UID);
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
