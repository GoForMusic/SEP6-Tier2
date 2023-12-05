using System.ComponentModel.DataAnnotations;

namespace Shared.SpecialCases;

/// <summary>
/// Special class for REST Update/Create to not ask the user to intro all the elements only the necessary ones
/// </summary>
public class CommentREST
{
    /// <summary>
    /// PK
    /// </summary>
    [Required] 
    public long Id { get; set; }
    
    /// <summary>
    /// Content of the comment required as we do not allowed empty messages
    /// </summary>
    [Required]
    public string Body { get; set; }
    /// <summary>
    /// DateTime
    /// </summary>
    public DateTime? date_posted { get; set; } = DateTime.Now;
    public long account_id { get; set; }
    public long movie_id { get; set; }

    public CommentREST()
    {
    }
}