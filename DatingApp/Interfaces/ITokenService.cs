using DatingApp.Data;

namespace DatingApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
