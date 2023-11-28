using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
    /// <summary>
    /// Account class or user class
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        [ForeignKey("Movie")]
        public ICollection<Movie> WatchList { get; set; }
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