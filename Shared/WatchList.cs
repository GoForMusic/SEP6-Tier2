using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared;

public class WatchList
{
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
    public People account_id { get; set; }
}