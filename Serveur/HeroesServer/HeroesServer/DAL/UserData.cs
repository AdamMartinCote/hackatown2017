using HeroesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace HeroesServer.DAL
{
    public class UserData
    {
        public static List<User> Get()
        {
            return null;
        }

        public static User Get(int id)
        {
            User user = new User();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source = mssql3.gear.host; Initial Catalog = heroes; Persist Security Info = True; User ID = heroes; Password = hackatown_heroes";
                conn.Open();

                // Create the command
                SqlCommand command = new SqlCommand("SELECT  u.[IdUser] "+
                                                          ",[Nom] " +
                                                          ",[Prenom] " +
                                                          ",[NiveauCompetence] " +
	                                                      ",p.Latitude " +
	                                                      ",p.Longitude " +
	                                                      ",p.LastUpdate " +
                                                      "FROM [heroes].[dbo].[User] u " +
                                                      "JOIN [dbo].[Position] p " +
                                                      "ON p.IdUser = u.IdUser " +
                                                      "WHERE u.IdUser = @idUser ", conn);
                // Add the parameters.
                command.Parameters.Add(new SqlParameter("idUser", id));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // while there is another record present
                    while (reader.Read())
                    {
                        user.idUser = int.Parse(reader[0].ToString());
                        user.Nom = reader[1].ToString();
                        user.Prenom = reader[2].ToString();
                        user.NiveauCompetence = (NiveauCompetence) int.Parse(reader[3].ToString());
                        Localisation loc = new Localisation();
                        loc.Latitude = float.Parse(reader[4].ToString());
                        loc.Longitude = float.Parse(reader[5].ToString());
                        loc.LastUpdate = DateTime.Parse(reader[6].ToString());
                        user.Position = loc;
                    }
                }
            }
            return user;
        }

        public static User Insert(User user)
        {
            return null;
        }

        public static User UpdatePositionUser(int id, float longitude, float latitude)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Data Source = mssql3.gear.host; Initial Catalog = heroes; Persist Security Info = True; User ID = heroes; Password = hackatown_heroes";
                conn.Open();

                // Create the command
                SqlCommand command = new SqlCommand("UPDATE Position " +
                                                      "SET   LastUpdate = GETDATE(), " +
                                                            "Latitude = @lat, " +
                                                            "Longitude = @long " +
                                                      "WHERE IdUser = @idUser", conn);
                // Add the parameters.
                command.Parameters.Add(new SqlParameter("idUser", id));
                command.Parameters.Add(new SqlParameter("lat", latitude));
                command.Parameters.Add(new SqlParameter("long", longitude));
                command.ExecuteNonQuery();
            }
            return null;
        }

        public static User Update(int id)
        {
            return null;
        }


        public static User Delete(int id)
        {
            return null;
        }
    }
}