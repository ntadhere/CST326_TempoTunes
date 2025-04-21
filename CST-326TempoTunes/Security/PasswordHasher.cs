namespace CST_326TempoTunes.Security
{
    /// <summary>
    /// Thin wrapper around BCrypt so we can swap algorithms later
    /// without touching controllers / DAOs.
    /// </summary>
    public class PasswordHasher
    {
        /// <summary>
        /// How much work BCrypt does (10‑14 is common; higher = slower).
        /// </summary>
        private const int WorkFactor = 12;

        /// <summary>
        /// Hashes a plaintext password using BCrypt with an embedded salt.
        /// </summary>
        public static string Hash(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText, WorkFactor);
        }

        /// <summary>
        /// Verifies a plaintext password against a previously generated hash.
        /// </summary>
        public static bool Verify(string plainText, string storedHash)
        {
            // If the DB accidentally contains a non‑hash value, fail fast.
            if (string.IsNullOrWhiteSpace(storedHash) || !storedHash.StartsWith("$2"))
                return false;

            return BCrypt.Net.BCrypt.Verify(plainText, storedHash);
        }
    }
}
