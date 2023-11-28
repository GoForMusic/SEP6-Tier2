namespace Shared
{
    /// <summary>
    /// Account class or user class
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A list of movies saved.
        /// </summary>
        public List<Movie> WatchList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        public Account(string username, string password, int id)
        {
            Password = password;
            UserName = username;
            Id = id;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Account()
        {
        }
    }
}