using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CST_326TempoTunes.Models;
using CST_326TempoTunes.Security;          // <‑‑ NEW (for PasswordHasher)
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CST_326TempoTunes.Services.DataAccess
{
    /// <summary>
    /// Low‑level CRUD operations for the “users” collection.
    /// Handles auto‑ID generation and password hashing.
    /// </summary>
    public class UserDAO
    {
        private readonly IMongoCollection<UserModel> users;

        public UserDAO(IConfiguration config)
        {
            var connString = config.GetConnectionString("MONGO_CONNECTION");
            if (string.IsNullOrWhiteSpace(connString))
                throw new InvalidOperationException("Missing MongoConnection in configuration.");

            var settings = MongoClientSettings.FromConnectionString(connString);
            settings.AllowInsecureTls = true;   // dev only; remove for prod
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(10);

            var client = new MongoClient(settings);
            var database = client.GetDatabase("cst326");
            users = database.GetCollection<UserModel>("users");
        }

        /* --------------------  READ  -------------------- */

        /// <summary>
        /// Returns the first user whose Username matches (case‑insensitive).
        /// </summary>
        public UserModel? GetUserByUsername(string username)
        {
            var filter = Builders<UserModel>.Filter.Regex(
                u => u.Username,
                new BsonRegularExpression($"^{Regex.Escape(username)}$", "i"));

            return users.Find(filter).FirstOrDefault();
        }

        public List<UserModel> ReadAllUsers() =>
            users.Find(FilterDefinition<UserModel>.Empty).ToList();

        /* --------------------  CREATE  -------------------- */

        public bool CreateUser(UserModel user)
        {
            // Application‑level auto‑increment
            if (user.Id == 0)
                user.Id = GetNextAppId();

            // Hash the password if we were given plain text
            if (!user.Password.StartsWith("$2"))
                user.Password = PasswordHasher.Hash(user.Password);

            users.InsertOne(user);
            return user.MongoId != null;    // MongoDB assigned _id
        }

        /* --------------------  UPDATE  -------------------- */

        public bool UpdateUser(UserModel user)
        {
            // Re‑hash only when caller passes a fresh plain‑text password
            if (!user.Password.StartsWith("$2"))
                user.Password = PasswordHasher.Hash(user.Password);

            var result = users.ReplaceOne(u => u.Id == user.Id, user);
            return result.ModifiedCount > 0;
        }

        /* --------------------  DELETE  -------------------- */

        public bool DeleteUser(int id) =>
            users.DeleteOne(u => u.Id == id).DeletedCount > 0;

        /* --------------------  HELPERS  -------------------- */

        private int GetNextAppId()
        {
            var sort = Builders<UserModel>.Sort.Descending(u => u.Id);
            var lastUser = users.Find(FilterDefinition<UserModel>.Empty).Sort(sort).FirstOrDefault();
            return (lastUser?.Id ?? 0) + 1;
        }
    }
}
