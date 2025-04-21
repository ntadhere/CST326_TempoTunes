using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.DataAccess;
using System.Data.SqlClient;

namespace CST_326TempoTunes.Services.Business
{
    public class PlaylistCollection
    {
        private readonly PlaylistDAO dao;

        public PlaylistCollection(PlaylistDAO dao)
        {
            this.dao = dao;
        }

        public List<PlaylistModel> GetAllPlaylists() => dao.ReadAllPlaylist();
        public List<TrackModel> GetAllTracks() => dao.ReadAllTracks();
        public List<TrackModel> GetTracksForPlaylist(int id) => dao.ReadTracksForPlaylist(id);
        public bool RemovePlaylist(int id) => dao.RemovePlaylist(id);
        public bool RemoveTrack(int id) => dao.RemoveTrack(id);
        public bool AddPlaylist(PlaylistModel m) => dao.AddPlaylist(m);
        public bool AddTrack(TrackModel t, int pid) => dao.AddTrack(t, pid);
        public bool UpdatePlaylist(PlaylistModel m) => dao.UpdatePlaylist(m);
        public bool UpdateTrack(TrackModel t) => dao.UpdateTrack(t);

    }

}
