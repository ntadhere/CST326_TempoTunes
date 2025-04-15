using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.DataAccess;
using System.Data.SqlClient;

namespace CST_326TempoTunes.Services.Business
{
    public class PlaylistCollection
    {
        private readonly PlaylistDAO playlistDAO;

        public PlaylistCollection()
        {
            // Instantiate the DAO
            playlistDAO = new PlaylistDAO();
        }

        // This method is used by the controller to get all playlists.
        public List<PlaylistModel> GetAllPlaylists()
        {
            return playlistDAO.ReadAllPlaylist();
        }

        // This method is used by the controller to get all tracks
        public List<TrackModel> GetAllTracks()
        {
            return playlistDAO.ReadAllTracks();
        }

        // New method to retrieve tracks for a chosen playlist
        public List<TrackModel> GetTracksForPlaylist(int playlistId)
        {
            return playlistDAO.ReadTracksForPlaylist(playlistId);
        }
        public bool RemovePlaylist(int playlistId)
        {
            return playlistDAO.RemovePlaylist(playlistId);
        }

        public bool RemoveTrack(int trackId)
        {
            return playlistDAO.RemoveTrack(trackId);
        }
        /// <summary>
        /// Adds a new playlist to the database.
        /// </summary>
        public bool AddPlaylist(PlaylistModel playlist)
        {
            return playlistDAO.AddPlaylist(playlist);
        }

        /// <summary>
        /// Adds a new track to the specified playlist.
        /// </summary>
        public bool AddTrack(TrackModel track, int playlistId)
        {
            return playlistDAO.AddTrack(track, playlistId);
        }
    }

}
