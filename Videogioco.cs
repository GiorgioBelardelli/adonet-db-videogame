using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Numerics;
using System.Xml.Linq;

namespace adonet_db_videogame
{
    
    public class Videogioco
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime Release_date { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int Software_house_id { get; set; }

        public Videogioco(string name, string overview, DateTime release_date, DateTime created_at, DateTime updated_at, int software_house_id)
        {
            Name = name;
            Overview = overview;
            Release_date = release_date;
            Created_at = created_at;
            Updated_at = updated_at;
            Software_house_id = software_house_id;
        }
    }

    public class VideogameManager
    {
        public const string STRINGA_DI_CONNESSIONE = "Data Source=localhost;Initial Catalog=db-videogames;Integrated Security=True;";


        public void InserisciVideogioco(Videogioco videogioco)
        {
            using (SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE))
            {
                try
                {
                    // CONTROLLI
                    if (string.IsNullOrEmpty(videogioco.Name))
                    {
                        throw new ArgumentException("Il nome del videogioco non può essere vuoto.");
                    }
                    if (string.IsNullOrEmpty(videogioco.Overview))
                    {
                        throw new ArgumentException("L'overview del videogioco non può essere vuoto.");
                    }
                    if (videogioco.Software_house_id < 1 || videogioco.Software_house_id > 6)
                    {
                        throw new ArgumentException("L'ID della software house deve essere compreso tra 1 e 6.");
                    }


                    connessioneSql.Open();

                    string query = @"INSERT INTO videogames(name, overview, release_date, created_at, updated_at, software_house_id)
                                 VALUES(@Name, @Overview, @Release_date, @Created_at, @Updated_at, @Software_house_id)";

                    using (SqlCommand cmd = new SqlCommand(query, connessioneSql))
                    {
                        cmd.Parameters.AddWithValue("@Name", videogioco.Name);
                        cmd.Parameters.AddWithValue("@Overview", videogioco.Overview);
                        cmd.Parameters.AddWithValue("@Release_date", videogioco.Release_date);
                        cmd.Parameters.AddWithValue("@Created_at", videogioco.Created_at);
                        cmd.Parameters.AddWithValue("@Updated_at", videogioco.Updated_at);
                        cmd.Parameters.AddWithValue("@Software_house_id", videogioco.Software_house_id);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void GetVideogameById(int id)
        {
            using (SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE))
                try
                {
                    connessioneSql.Open();

                    string query = @"SELECT videogames.name as VideogameName, videogames.release_date as VideogameReleaseDate
                               FROM videogames
                               WHERE id = @id";

                    using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    using SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        int indiceVideogameName = reader.GetOrdinal("VideogameName");
                        int indiceReleaseDate = reader.GetOrdinal("VideogameReleaseDate");
                        Console.WriteLine($"Videogame {reader.GetString(indiceVideogameName)}, Rilasciato il {reader.GetDateTime(indiceReleaseDate)}");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public void SearchVideogamesByName(string userSearch)
        {
            using (SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE))
                try
                {
                    connessioneSql.Open();

                    string query = @"SELECT videogames.name as VideogameName, videogames.release_date as VideogameReleaseDate
                                    FROM videogames
                                    WHERE name LIKE @searchString";

                    using SqlCommand cmd = new SqlCommand(query, connessioneSql);

                    cmd.Parameters.Add(new SqlParameter("@searchString", "%" + userSearch + "%"));

                    using SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        int indiceVideogameName = reader.GetOrdinal("VideogameName");
                        int indiceReleaseDate = reader.GetOrdinal("VideogameReleaseDate");
                        Console.WriteLine($"Videogame {reader.GetString(indiceVideogameName)}, Rilasciato il {reader.GetDateTime(indiceReleaseDate)}");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public void DeleteVideoGameById(long id)
        {
            using (SqlConnection connessioneSql = new SqlConnection(STRINGA_DI_CONNESSIONE))
                try 
                {
                    connessioneSql.Open();
                    
                    string query = @"DELETE 
                                    FROM videogames
                                    WHERE id = @id";
                    using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                    
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                                     
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }
    }
}
