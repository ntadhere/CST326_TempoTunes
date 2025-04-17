using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CST_326TempoTunes.Models
{
    /// <summary>
    /// Maps to a document in the  “users”  collection.
    /// </summary>
    public class UserModel
    {
        // MongoDB’s own primary key. You don’t have to use it in your app logic,
        // but keeping it lets you work with Mongo utilities and indexes.
        [BsonId]                                      // marks this as the _id field
        [BsonRepresentation(BsonType.ObjectId)]       // store as string in C#
        public string? MongoId { get; set; }          // e.g. "6800a9fdf4cc6962967fecca"

        // Your application‑level numeric Id (matches the “id” field in the document).
        [BsonElement("id")]
        public int Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        // Store the hashed password here (never plaintext in production).
        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;
    }
}
