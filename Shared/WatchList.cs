using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared;

public class WatchList
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    public long? Id { get; set; }

    /// <summary>
    /// The id of the movie
    /// </summary>
    [Required]
    [ForeignKey("Movie")]
    public Movie movie_id { get; set; }

    /// <summary>
    /// The id of the person
    /// </summary>
    [Required]
    [ForeignKey("Account")]
    public Account account_id { get; set; }

    public WatchList()
    {
        // Set default values for the properties here
        movie_id = new Movie(); // Example initialization, replace with appropriate logic
        account_id = new Account(); // Example initialization, replace with appropriate logic
    }

}