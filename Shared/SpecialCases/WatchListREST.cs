namespace Shared.SpecialCases;

/// <summary>
/// Special class for REST Update/Create to not ask the user to intro all the elements only the necessary ones
/// </summary>
public class WatchListREST
{
    public long? Id { get; set; }
    public long account_id { get; set; }
    public long movie_id { get; set; }

    public WatchListREST()
    {
    }
}