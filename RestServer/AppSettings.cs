using System.ComponentModel.DataAnnotations;

namespace RestServer;

/// <summary>
/// A class that predefine variables from a JSON file
/// </summary>
public class AppSettings
{
    /// <summary>
    /// DATABASE connection string (host/user/pass/db)
    /// </summary>
    [Required]
    public static string DATABASE_CONNECTION_STRING { get; private set; }
}