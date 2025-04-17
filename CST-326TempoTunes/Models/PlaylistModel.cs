using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CST_326TempoTunes.Models
{
    public class PlaylistModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string ImageUrl { get; set; }

        // This field will not be stored in the playlist collection,
        // but can be populated manually in your service layer if needed.
        [BsonIgnore]
        public List<TrackModel> Tracks { get; set; }
    }

}
