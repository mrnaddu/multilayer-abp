namespace IVP.DbMigrator.OpenIddict;

public class ClientApplication
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string RootUrl { get; set; }
    public string[] Scopes { get; set; }
    public string[] GrantTypes { get; set; }
    public string[] RedirectUris { get; set; }
    public string[] PostLogoutRedirectUris { get; set; }
}
