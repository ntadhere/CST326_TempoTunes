using System;
using System.Collections.Generic;
using CST_326TempoTunes.Models;
using Microsoft.Extensions.Configuration;
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
            return playlists.Find(FilterDefinition<PlaylistModel>.Empty).ToList();
        }

        // 4. Read all tracks for a given playlist
        public List<TrackModel> ReadTracksForPlaylist(int playlistId)
        {
            return tracks.Find(t => t.PlaylistId == playlistId).ToList();
        }

        // 5. Read all tracks in the DB
        public List<TrackModel> ReadAllTracks()
        {
            return tracks.Find(FilterDefinition<TrackModel>.Empty).ToList();
        }

        // 6. Remove a playlist (and its tracks)
        public bool RemovePlaylist(int playlistId)
        {
            tracks.DeleteMany(t => t.PlaylistId == playlistId);
            var result = playlists.DeleteOne(p => p.Id == playlistId);
            return result.DeletedCount > 0;
        }

        // 7. Remove a single track
        public bool RemoveTrack(int trackId)
        {
            var result = tracks.DeleteOne(t => t.Id == trackId);
            return result.DeletedCount > 0;
        }

        // 8. Add a new playlist and capture a unique Id
        public bool AddPlaylist(PlaylistModel playlist)
        {
            if (playlist.Id == 0)
                playlist.Id = GetNextPlaylistId();

            playlists.InsertOne(playlist);
            return playlist.Id != 0;
        }

        // 9. Add a new track to a playlist
        public bool AddTrack(TrackModel track, int playlistId)
        {
            if (track.Id == 0)
                track.Id = GetNextTrackId();

            track.PlaylistId = playlistId;
            tracks.InsertOne(track);
            return track.Id != 0;
        }

        public bool UpdatePlaylist(PlaylistModel playlist)
        {
            var filter = Builders<PlaylistModel>.Filter.Eq(p => p.Id, playlist.Id);

            var update = Builders<PlaylistModel>.Update
                .Set(p => p.Name, playlist.Name)
                .Set(p => p.ArtistName, playlist.ArtistName)
                .Set(p => p.ImageUrl, playlist.ImageUrl);

            var result = playlists.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }

        public bool UpdateTrack(TrackModel track)
        {
            var filter = Builders<TrackModel>.Filter.Eq(t => t.Id, track.Id);
            var result = tracks.ReplaceOne(filter, track);
            return result.ModifiedCount > 0;
        }

        // Helpers for auto-increment
        private int GetNextPlaylistId()
        {
            var sort = Builders<PlaylistModel>.Sort.Descending(p => p.Id);
            var last = playlists.Find(FilterDefinition<PlaylistModel>.Empty)
                                .Sort(sort)
                                .FirstOrDefault();
            return (last?.Id ?? 0) + 1;
        }

        private int GetNextTrackId()
        {
            var sort = Builders<TrackModel>.Sort.Descending(t => t.Id);
            var last = tracks.Find(FilterDefinition<TrackModel>.Empty)
                             .Sort(sort)
                             .FirstOrDefault();
            return (last?.Id ?? 0) + 1;
        }
    }
}
