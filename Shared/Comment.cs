using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public DateTime? date_posted { get; set; } = DateTime.Now;
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

    public Comment()
    {
    }
}