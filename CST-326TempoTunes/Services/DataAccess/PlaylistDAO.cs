using System.Collections.Generic;
using CST_326TempoTunes.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CST_326TempoTunes.Services.DataAccess
{
    public class PlaylistDAO
    {
        // 1. MongoDB connection string and database/collection names
        private readonly string mongoConnectionString = "mongodb+srv://dorothy:cst326@tempotunes.fp9e8r3.mongodb.net/?retryWrites=true&w=majority&appName=TempoTunes";
        private readonly string databaseName = "cst326";
        private readonly string playlistCollectionName = "playlists";
        private readonly string trackCollectionName = "tracks";

        private readonly IMongoCollection<PlaylistModel> playlists;
        private readonly IMongoCollection<TrackModel> tracks;

        public PlaylistDAO()
        {
            // 2. Initialize Mongo client and get collections
            var client = new MongoClient(mongoConnectionString);
            var db = client.GetDatabase(databaseName);
            playlists = db.GetCollection<PlaylistModel>(playlistCollectionName);
            tracks = db.GetCollection<TrackModel>(trackCollectionName);
        }

        // 3. Read all playlists
        public List<PlaylistModel> ReadAllPlaylist()
        {
            return playlists.Find(FilterDefinition<PlaylistModel>.Empty)
                            .ToList();
        }

        // 4. Read all tracks for a given playlist
        public List<TrackModel> ReadTracksForPlaylist(int playlistId)
        {
            return tracks.Find(t => t.PlaylistId == playlistId)
                         .ToList();
        }

        // 5. Read all tracks in the DB
        public List<TrackModel> ReadAllTracks()
        {
            return tracks.Find(FilterDefinition<TrackModel>.Empty)
                         .ToList();
        }

        // 6. Remove a playlist (and its tracks)
        public bool RemovePlaylist(int playlistId)
        {
            // Delete tracks first
            var deleteTracksResult = tracks.DeleteMany(t => t.PlaylistId == playlistId);
            // Then delete the playlist
            var deletePlaylistResult = playlists.DeleteOne(p => p.Id == playlistId);
            return deletePlaylistResult.DeletedCount > 0;
        }

        // 7. Remove a single track
        public bool RemoveTrack(int trackId)
        {
            var result = tracks.DeleteOne(t => t.Id == trackId);
            return result.DeletedCount > 0;
        }

        // 8. Add a new playlist and capture the generated Id
        public bool AddPlaylist(PlaylistModel playlist)
        {
            // In MongoDB, unless you set Id yourself, it will be generated.
            playlists.InsertOne(playlist);
            return playlist.Id != 0;
        }

        // 9. Add a new track to a playlist
        public bool AddTrack(TrackModel track, int playlistId)
        {
            track.PlaylistId = playlistId;
            tracks.InsertOne(track);
            return track.Id != 0;
        }
    }
}