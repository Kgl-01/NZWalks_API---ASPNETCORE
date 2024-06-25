using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {

        string CreateJwtToken(IdentityUser user, IEnumerable<string> roles);

    }
}
