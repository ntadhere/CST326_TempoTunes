using System.Collections.Generic;
using CST_326TempoTunes.Models;
using CST_326TempoTunes.Services.DataAccess;
using System.Data.SqlClient;   // ← kept for symmetry with PlaylistCollection (not actually used here)

namespace CST_326TempoTunes.Services.Business
{
    /// <summary>
    /// Business‑layer façade for working with users.
    /// </summary>
    public class UserCollection
    {
        private readonly UserDAO dao;

        public UserCollection(UserDAO dao)
        {
            this.dao = dao;
        }

        /* --------------------  READ  -------------------- */

        /// <summary>
        /// Returns a user whose Username matches (case‑insensitive); null if not found.
        /// </summary>
        public UserModel? GetUserByUsername(string username) => dao.GetUserByUsername(username);

        /// <summary>
        /// Convenience wrapper in case you expose a “list users” page.
        /// </summary>
        public List<UserModel> GetAllUsers() => dao.ReadAllUsers();

        /* --------------------  CREATE  -------------------- */

        public bool AddUser(UserModel user) => dao.CreateUser(user);

        /* --------------------  UPDATE  -------------------- */

        public bool UpdateUser(UserModel user) => dao.UpdateUser(user);

        /* --------------------  DELETE  -------------------- */

        public bool DeleteUser(int id) => dao.DeleteUser(id);
    }
}
