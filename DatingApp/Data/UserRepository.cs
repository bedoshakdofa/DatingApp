using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Data.Models;
using DatingApp.DTOs;
using DatingApp.Helpers;
using DatingApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Data
{
    public class UserRepository:IRepository
    {
        private readonly DbContextApplication _context;

        private readonly IMapper _mapper;
        public UserRepository(DbContextApplication context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<MemberDto>> GetAllUser(UserParams userParams)
        {
            var query = _context.users.AsQueryable();

            query=query.Where(u=>u.UserName!=userParams.Username);
            query = query.Where(u => u.Gender == userParams.Gender);

            query = userParams.OrderBy switch
            {
                "Created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive)
            };

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
                userParams.pageNumber,userParams.pageSize);
        }

        public async Task<User>GetUserByUsername(string Username)
        {
            return await _context.users.Include(x=>x.Photos).SingleOrDefaultAsync(x=>x.UserName==Username);
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUserbyIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }
    }
};