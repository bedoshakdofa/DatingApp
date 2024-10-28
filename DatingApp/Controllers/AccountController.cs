using DatingApp.Data;
using DatingApp.DTOs;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    public class AccountController:baseApiController
    {
        private readonly DbContextApplication _context;

        private readonly ITokenService _JwtToken;

        public AccountController(DbContextApplication context,ITokenService Token)
        {
            _context = context;
            _JwtToken = Token;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>>sginUp(RegisterDto registerDto)
        {
            if (await checkUserExist(registerDto.email)) return BadRequest("user is found");

            using var hmac = new HMACSHA512();
            var user = new User
            {
                userName=registerDto.Username.ToLower(),
                User_email=registerDto.email.ToLower(),
                hashedPass=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                saltPassword=hmac.Key

            };
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = user.userName,
                Token =_JwtToken.CreateToken(user)
            };
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> login(LoginDTO loginDto)
        {
            User user1=await _context.users.SingleOrDefaultAsync(x=>x.User_email==loginDto.email);
            if (user1 == null) return Unauthorized("invaild email");

            using var hmac=new HMACSHA512(user1.saltPassword);

            var computedhash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < computedhash.Length; i++)
            {
                if (computedhash[i] != user1.hashedPass[i]) return Unauthorized("invaild password");
            }

            return new UserDto
            {
                UserName = user1.userName,
                Token = _JwtToken.CreateToken(user1)
            };
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<ActionResult<User>> getUserById(int id)
        {
            return Ok(await _context.users.FindAsync(id));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>>GetAllUser()
        {
            return Ok(await _context.users.ToListAsync());
        }

        private async Task<bool> checkUserExist(string email)
        {
            return await _context.users.AnyAsync(x => x.User_email == email.ToLower());
        }
    }
}

