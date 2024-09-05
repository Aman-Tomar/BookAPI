using Microsoft.AspNetCore.Identity;

namespace Books.API.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(IdentityUser user);
    }
}
