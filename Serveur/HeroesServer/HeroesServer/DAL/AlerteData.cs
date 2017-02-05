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
        public static Alerte GetByIDInitiateur(int idInitiateur)
        {
            Alerte alerte = new Alerte();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source = mssql3.gear.host; Initial Catalog = heroes; Persist Security Info = True; User ID = heroes; Password = hackatown_heroes";
                conn.Open();

                // Create the command
                SqlCommand command = new SqlCommand(@"  SELECT
                                                         a.*
                                                        , p.Latitude
                                                        , p.Longitude
                                                        , p.LastUpdate
                                                        FROM Alerte a
                                                        JOIN Position p
                                                        ON p.IdAlerte = a.IdAlerte
                                                        "+"WHERE IdInitiateur = @idInitiateur", conn);
                // Add the parameters.
                command.Parameters.Add(new SqlParameter("idInitiateur", idInitiateur));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {
                        alerte.IdAlert = int.Parse(reader[0].ToString());
                        alerte.IdInitiateur = int.Parse(reader[1].ToString());
                        alerte.IdRepondant = reader[2] == DBNull.Value ? 0 : int.Parse(reader[2].ToString());
                        alerte.Gravite = (Gravite)int.Parse(reader[3].ToString());
                        alerte.Type = (TypeAlerte)int.Parse(reader[4].ToString());
                        alerte.IsAnswered = reader[5].ToString() == "True";
                        Localisation loc = new Localisation();
                        loc.Latitude = float.Parse(reader[6].ToString());
                        loc.Longitude = float.Parse(reader[7].ToString());
                        loc.LastUpdate = DateTime.Parse(reader[8].ToString());
                        alerte.Position = loc;
                    }
                }
            }
            return alerte;
        }

        public static Alerte Insert(Alerte alerte)
        {
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source = mssql3.gear.host; Initial Catalog = heroes; Persist Security Info = True; User ID = heroes; Password = hackatown_heroes";
                conn.Open();

                // Create the command
                SqlCommand command = new SqlCommand(@"  INSERT INTO [dbo].[Alerte]
                                                       ([IdInitiateur]
                                                       ,[IdRepondant]
                                                       ,[Gravite]
                                                       ,[Type]
                                                       ,[IsAnswered])
                                                        VALUES " +
                                                       "(@idInit " +
                                                       ",null " +
                                                       ",@gravite " +
                                                       ",@type " +
                                                       ",0)", conn);
                command.Parameters.Add(new SqlParameter("idInit", alerte.IdInitiateur));
                command.Parameters.Add(new SqlParameter("gravite", alerte.Gravite));
                command.Parameters.Add(new SqlParameter("type", alerte.Type));
                command.ExecuteNonQuery();

                command = new SqlCommand(@"  INSERT INTO [dbo].[Position]
                                                       ([IdAlerte]
                                                       ,[Latitude]
                                                       ,[Longitude]
                                                       ,[LastUpdate])
                                                        VALUES
                                                       ((SELECT IDENT_CURRENT('[dbo].[Alerte]'))  " +
                                                       ",@lat"+
                                                       ",@long " +
                                                       @",GETDATE()
                                                        )", conn);
                command.Parameters.Add(new SqlParameter("lat", alerte.Position.Latitude));
                command.Parameters.Add(new SqlParameter("long", alerte.Position.Longitude));
                command.ExecuteNonQuery();

            }

            return alerte;
        }


        public static void Update(int idRepondant, int idInitiateur)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source = mssql3.gear.host; Initial Catalog = heroes; Persist Security Info = True; User ID = heroes; Password = hackatown_heroes";
                conn.Open();

                // Create the command
                SqlCommand command = new SqlCommand(@" 
                                                        UPDATE [dbo].[Alerte]
                                                           "+ "SET [IdRepondant] = @idRepondant "+
                                                              ",[IsAnswered] = 1 "+
                                                         "WHERE [IdInitiateur] = @idInitiateur ", conn);
                command.Parameters.Add(new SqlParameter("idRepondant", idRepondant));
                command.Parameters.Add(new SqlParameter("idInitiateur", idInitiateur));

                command.ExecuteNonQuery();
            }
            
        }


        public static Alerte Delete(int id)
        {
            return null;
        }
    }
}