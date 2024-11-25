using DatingApp.Data.Models;
using DatingApp.DTOs;
using DatingApp.Helpers;

namespace DatingApp.Interfaces
{
    public interface IRepository
    {
        Task<PagedList<MemberDto>> GetAllUser(UserParams userParams);

        Task<User> GetUserByUsername(string Username);

        Task<bool> SaveAllAsync();

        Task<User> GetUserbyIdAsync(int id); 
    }
}
