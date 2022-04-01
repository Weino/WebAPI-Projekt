using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Text.Encodings.Web;
using WebAPI_Projekt.Models;


namespace WebAPI_Projekt.APIAuth
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource("auth.web.api")
        };
            
        public static IEnumerable<Client> Clients = new List<Client>
        {
            new Client
            {
                ClientId = "ApiClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new [] { new Secret("apisecret".ToSha256()) },
                AllowedScopes = new [] { "auth.web.api" }
            }
        };
    }
}