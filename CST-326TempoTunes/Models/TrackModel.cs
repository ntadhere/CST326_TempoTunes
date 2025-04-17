using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CST_326TempoTunes.Models
{
    public class TrackModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Duration { get; set; }
    }
}
