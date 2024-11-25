using DatingApp.Data.Models;
using DatingApp.DTOs;

namespace DatingApp.Interfaces
{
    public interface ILikeRespository
    {
        Task<UserLikes> GetTheLike(int sourceUserId,int targerUserId);

        Task<User> GetUserWithLikes(int UserID);

        Task<IEnumerable<UserLikesDto>> GetUserLikes(string predicate,int userId);
    }
}
