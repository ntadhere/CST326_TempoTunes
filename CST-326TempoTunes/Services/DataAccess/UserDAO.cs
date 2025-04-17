using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CST_326TempoTunes.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CST_326TempoTunes.Services.DataAccess
{
    /// <summary>
    /// Low‑level CRUD operations for the “users” collection.
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
            settings.AllowInsecureTls = true;              // ⚠️ for dev environments only
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(10);

            var client = new MongoClient(settings);
            var db = client.GetDatabase("cst326");
            users = db.GetCollection<UserModel>("users");
        }

        /* --------------------  READ  -------------------- */

        /// <summary>
        /// Returns the first user whose Username matches (case‑insensitive).  
        /// Returns null if no match is found.
        /// </summary>
        public UserModel? GetUserByUsername(string username)
        {
            var filter = Builders<UserModel>.Filter.Regex(
                u => u.Username,
                new BsonRegularExpression($"^{Regex.Escape(username)}$", "i"));

            return users.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// Returns every user in the collection. Useful for admin pages.
        /// </summary>
        public List<UserModel> ReadAllUsers() =>
            users.Find(FilterDefinition<UserModel>.Empty).ToList();

        /* --------------------  CREATE  -------------------- */

        /// <summary>
        /// Inserts a new user document.  
        /// Returns true if the insert succeeded.
        /// </summary>
        public bool CreateUser(UserModel user)
        {
            // If you rely on an integer Id, generate the next one here.
            if (user.Id == 0)
            {
                user.Id = GetNextAppId();
            }

            users.InsertOne(user);
            return user.MongoId != null;    // MongoDB generated an _id
        }

        /* --------------------  UPDATE  -------------------- */

        /// <summary>
        /// Replaces the document that has the same numeric Id.  
        /// Returns true if a document was modified.
        /// </summary>
        public bool UpdateUser(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Id, user.Id);
            var result = users.ReplaceOne(filter, user);
            return result.ModifiedCount > 0;
        }

        /* --------------------  DELETE  -------------------- */

        /// <summary>
        /// Removes the user whose numeric Id matches.  
        /// Returns true if a document was deleted.
        /// </summary>
        public bool DeleteUser(int id)
        {
            var result = users.DeleteOne(u => u.Id == id);
            return result.DeletedCount > 0;
        }

        /* --------------------  HELPERS  -------------------- */

        /// <summary>
        /// Simple helper to auto‑increment the application‑level Id.
        /// </summary>
        private int GetNextAppId()
        {
            var sort = Builders<UserModel>.Sort.Descending(u => u.Id);
            var lastUser = users.Find(FilterDefinition<UserModel>.Empty).Sort(sort).FirstOrDefault();
            return (lastUser?.Id ?? 0) + 1;
        }
    }
}
