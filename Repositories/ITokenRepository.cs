using Microsoft.AspNetCore.Identity;

namespace NGWALKSAPI.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);


    }
}
