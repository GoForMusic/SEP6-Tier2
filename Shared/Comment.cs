using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Shared;

public class Comment
{
    /// <summary>
    /// PK
    /// </summary>
    [Key] 
    public long? Id { get; set; }
    
    /// <summary>
    /// Content of the comment required as we do not allowed empty messages
    /// </summary>
    [Required]
    public string Body { get; set; }
    /// <summary>
    /// DateTime
    /// </summary>
    public DateTime? date_posted { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Reference FK to Account who wrote the comment
    /// </summary>
    [ForeignKey("Account")]
    public Account WrittenBy { get; set; }
    /// <summary>
    /// Reference FK to Movie ID where will show the comment
    /// </summary>
    [ForeignKey("Movie")]
    public Movie movie_id { get; set; }

    /// <summary>
    /// Amount of likes
    /// </summary>
    public long? NumberOfLikes { get; set; } = 0;
    public Comment()
    {
        Body = string.Empty;
        WrittenBy = new Account();
        movie_id = new Movie();
    }
}