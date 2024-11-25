using DatingApp.Data.Models;
using DatingApp.DTOs;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class LikesRepository : ILikeRespository
    {
        private readonly DbContextApplication _context;

        public LikesRepository(DbContextApplication context)
        {
            _context = context;
        }
        public async Task<UserLikes> GetTheLike(int sourceUserId, int targerUserId)
        {
            return await _context.likes.FindAsync(sourceUserId, targerUserId);
        }

        public async Task<IEnumerable<UserLikesDto>> GetUserLikes(string predicate, int userId)
        {
            var user=_context.users.OrderBy(u=>u.UserName).AsQueryable();
            var likes=_context.likes.AsQueryable();

            if (predicate == "liked")
            {
                likes=likes.Where(l=>l.SourceUserId==userId);
                user = likes.Select(l => l.TragerUser);
            }
            else if (predicate == "likedBy")
            {
                likes=likes.Where(l=>l.TragetUserId==userId);
                user=likes.Select(l=>l.SourceUser);
            }

            return await user.Select(user => new UserLikesDto
            {
                UserName = user.UserName,
                City = user.City,
                KnownAs = user.KnownAs,
                Id = user.Id,
                photoUrl = user.Photos.FirstOrDefault(x => x.isMain).Url
            }).ToListAsync();
        }

        public async Task<User> GetUserWithLikes(int UserID)
        {
            return await _context.users.Include(x=>x.LikedUsers).FirstOrDefaultAsync(x=>x.Id==UserID);
        }
    }
}
