using System.Collections.Generic;
using CST_326TempoTunes.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace CST_326TempoTunes.Services.DataAccess
{
    public class PlaylistDAO
    {
        private readonly IMongoCollection<PlaylistModel> playlists;
        private readonly IMongoCollection<TrackModel> tracks;

        public PlaylistDAO(IConfiguration config)
        {
            var connString = config.GetConnectionString("MONGO_CONNECTION");
            if (string.IsNullOrWhiteSpace(connString))
                throw new InvalidOperationException("Missing MongoConnection in configuration.");

            var settings = MongoClientSettings.FromConnectionString(connString);
            settings.AllowInsecureTls = true;              // for Azure cert trust (remove in prod)
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(10);

            var client = new MongoClient(settings);
            var db = client.GetDatabase("cst326");
            playlists = db.GetCollection<PlaylistModel>("playlists");
            tracks = db.GetCollection<TrackModel>("tracks");
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
        /// <summary>
        /// Updates an existing playlist document by numeric Id.
        /// </summary>
        public bool UpdatePlaylist(PlaylistModel playlist)
        {
            var filter = Builders<PlaylistModel>.Filter.Eq(p => p.Id, playlist.Id);
            var result = playlists.ReplaceOne(filter, playlist);
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Updates an existing track document by numeric Id.
        /// </summary>
        public bool UpdateTrack(TrackModel track)
        {
            var filter = Builders<TrackModel>.Filter.Eq(t => t.Id, track.Id);
            var result = tracks.ReplaceOne(filter, track);
            return result.ModifiedCount > 0;
        }

    }
}