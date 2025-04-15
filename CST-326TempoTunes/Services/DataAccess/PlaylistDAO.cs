using CST_326TempoTunes.Models;
using MySql.Data.MySqlClient;

namespace CST_326TempoTunes.Services.DataAccess
{
    public class PlaylistDAO
    {
        // Update the connection string with your server, database, and credentials.
        private readonly string connectionString = "server=localhost;port=3306;database=cst326-music;user=root;password=root;";
        public List<PlaylistModel> ReadAllPlaylist()
        {
            List<PlaylistModel> playlists = new List<PlaylistModel>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Query to fetch all playlists
                string query = "SELECT Id, Name, ArtistName, ImageUrl FROM Playlist";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
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

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Query to fetch tracks by PlaylistId
                string query = "SELECT Id, Name, Artist, Duration FROM Track WHERE PlaylistId = @PlaylistId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
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
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Artist, Duration FROM Track";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
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

        // Method to remove a chosen playlist and its associated tracks
        public bool RemovePlaylist(int playlistId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Remove related tracks first (if cascade delete is not enabled)
                string deleteTracksQuery = "DELETE FROM Track WHERE PlaylistId = @PlaylistId";
                string deletePlaylistQuery = "DELETE FROM Playlist WHERE Id = @PlaylistId";
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(deleteTracksQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand(deletePlaylistQuery, conn))
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
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Track WHERE Id = @TrackId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrackId", trackId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected > 0;
                }
            }
        }

        // Method to add a new playlist and retrieve its generated ID
        public bool AddPlaylist(PlaylistModel playlist)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Use LAST_INSERT_ID() to get the auto-generated ID
                string query = @"
                    INSERT INTO Playlist (Name, ArtistName, ImageUrl) 
                    VALUES (@Name, @ArtistName, @ImageUrl);
                    SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", playlist.Name);
                    cmd.Parameters.AddWithValue("@ArtistName", playlist.ArtistName);
                    cmd.Parameters.AddWithValue("@ImageUrl", playlist.ImageUrl);

                    conn.Open();
                    // ExecuteScalar returns the value from SELECT LAST_INSERT_ID();
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Assign the new id to your playlist object
                    playlist.Id = newId;

                    return newId > 0;
                }
            }
        }

        // Method to add a new track and retrieve its generated ID
        public bool AddTrack(TrackModel track, int playlistId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // Use LAST_INSERT_ID() to get the auto-generated ID
                string query = @"
                    INSERT INTO Track (Name, Artist, Duration, PlaylistId) 
                    VALUES (@Name, @Artist, @Duration, @PlaylistId);
                    SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", track.Name);
                    cmd.Parameters.AddWithValue("@Artist", track.Artist);
                    cmd.Parameters.AddWithValue("@Duration", track.Duration);
                    cmd.Parameters.AddWithValue("@PlaylistId", playlistId);

                    conn.Open();
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Update the track object with the new id
                    track.Id = newId;

                    return newId > 0;
                }
            }
        }
    }
}