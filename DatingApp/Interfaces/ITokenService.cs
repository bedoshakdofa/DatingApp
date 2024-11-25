using DatingApp.Data.Models;

namespace DatingApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
