using CST_326TempoTunes.Models;
using System.Data.SqlClient;

namespace CST_326TempoTunes.Services.DataAccess
{
    public class PlaylistDAO
    {
        // Update the connection string with your server, database, and credentials.
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CST326Music;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<PlaylistModel> ReadAllPlaylist()
        {
            List<PlaylistModel> playlists = new List<PlaylistModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Query to fetch all playlists
                string query = "SELECT Id, Name, ArtistName, ImageUrl FROM Playlist";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlaylistModel playlist = new PlaylistModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ArtistName = reader["ArtistName"].ToString(),
                                ImageUrl = reader["ImageUrl"].ToString()
                            };

                            playlists.Add(playlist);
                        }
                    }
                }
            }

            return playlists;
        }

        // New method to read all tracks for a given playlist
        public List<TrackModel> ReadTracksForPlaylist(int playlistId)
        {
            List<TrackModel> tracks = new List<TrackModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Query to fetch tracks by PlaylistId
                string query = "SELECT Id, Name, Artist, Duration FROM Track WHERE PlaylistId = @PlaylistId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TrackModel track = new TrackModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Artist = reader["Artist"].ToString(),
                                Duration = reader["Duration"].ToString()
                            };
                            tracks.Add(track);
                        }
                    }
                }
            }

            return tracks;
        }

        // Read all the tracks in the database
        public List<TrackModel> ReadAllTracks()
        {
            var tracks = new List<TrackModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Artist, Duration FROM Track";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var track = new TrackModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Artist = reader["Artist"].ToString(),
                                Duration = reader["Duration"].ToString()
                            };
                            tracks.Add(track);
                        }
                    }
                }
            }
            return tracks;
        }
        // Method to remove a chosen playlist
        public bool RemovePlaylist(int playlistId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Remove related tracks first (if cascade delete is not enabled)
                string deleteTracksQuery = "DELETE FROM Track WHERE PlaylistId = @PlaylistId";
                string deletePlaylistQuery = "DELETE FROM Playlist WHERE Id = @PlaylistId";
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(deleteTracksQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand(deletePlaylistQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                    int affected = cmd.ExecuteNonQuery();
                    return affected > 0;
                }
            }
        }

        // Method to remove a chosen track
        public bool RemoveTrack(int trackId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Track WHERE Id = @TrackId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrackId", trackId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected > 0;
                }
            }
        }

        public bool AddPlaylist(PlaylistModel playlist)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Append a SELECT to retrieve the new identity after the INSERT
                string query = @"
            INSERT INTO Playlist (Name, ArtistName, ImageUrl) 
            VALUES (@Name, @ArtistName, @ImageUrl);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", playlist.Name);
                    cmd.Parameters.AddWithValue("@ArtistName", playlist.ArtistName);
                    cmd.Parameters.AddWithValue("@ImageUrl", playlist.ImageUrl);

                    conn.Open();
                    // ExecuteScalar returns the value from SELECT CAST(SCOPE_IDENTITY() AS int)
                    int newId = (int)cmd.ExecuteScalar();

                    // Assign the new id to your playlist object
                    playlist.Id = newId;

                    return newId > 0;
                }
            }
        }


        public bool AddTrack(TrackModel track, int playlistId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Include the SELECT to get the identity of the inserted track
                string query = @"
            INSERT INTO Track (Name, Artist, Duration, PlaylistId) 
            VALUES (@Name, @Artist, @Duration, @PlaylistId);
            SELECT CAST(SCOPE_IDENTITY() AS int);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", track.Name);
                    cmd.Parameters.AddWithValue("@Artist", track.Artist);
                    cmd.Parameters.AddWithValue("@Duration", track.Duration);
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);

                    conn.Open();
                    int newId = (int)cmd.ExecuteScalar();

                    // Update the track object with the new id
                    track.Id = newId;

                    return newId > 0;
                }
            }
        }


    }
}
