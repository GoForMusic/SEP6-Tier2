namespace Shared.SpecialCases;

public class RequestLogin
{
    public string username { get; set; }
    public string password { get; set; }

    public RequestLogin()
    {
        username = string.Empty;
        password = string.Empty;
    }
}