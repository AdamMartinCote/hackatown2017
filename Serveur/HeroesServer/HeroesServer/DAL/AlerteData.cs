using HeroesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace HeroesServer.DAL
{
    public static class AlerteData
    {
        public static List<Alerte> GetAllAlerteEnCours()
        {

            List<Alerte> alertes = new List<Alerte>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source = mssql3.gear.host; Initial Catalog = heroes; Persist Security Info = True; User ID = heroes; Password = hackatown_heroes";
                conn.Open();

                // Create the command
                SqlCommand command = new SqlCommand(@"   SELECT 
                                                         a.IdAlerte
                                                        , a.IdInitiateur
                                                        , a.IdRepondant
                                                        , a.Gravite
                                                        , a.Type
                                                        , a.IsAnswered
                                                        , p.Latitude
                                                        , p.Longitude 
                                                        FROM Alerte a 
                                                        JOIN Position p 
                                                        ON p.IdAlerte = a.IdAlerte 
                                                        WHERE IsAnswered = 0", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {
                        Alerte alerte = new Alerte();
                        alerte.IdAlert = int.Parse(reader[0].ToString());
                        alerte.IdInitiateur = int.Parse(reader[1].ToString());
                        alerte.IdRepondant = reader[2] == DBNull.Value ? 0 : int.Parse(reader[2].ToString());
                        alerte.Gravite = (Gravite)int.Parse(reader[3].ToString());
                        alerte.Type = (TypeAlerte)int.Parse(reader[4].ToString());
                        alerte.IsAnswered = reader[5].ToString() == "1";
                        Localisation loc = new Localisation();
                        loc.Latitude = float.Parse(reader[6].ToString());
                        loc.Longitude = float.Parse(reader[7].ToString());
                        alerte.Position = loc;
                        alertes.Add(alerte);
                    }
                }
            }
            
            return alertes;
        }

        public static List<Alerte> Get()
        {
            return null;
        }

        public static Alerte Get(int id)
        {
            return null;
        }
        public static Alerte GetByIDInitiator(int idInitiator)
        {
            return null;
        }

        public static Alerte Insert(Alerte alerte)
        {
            return null;
        }


        public static Alerte Update(int id)
        {
            return null;
        }


        public static Alerte Delete(int id)
        {
            return null;
        }
    }
}