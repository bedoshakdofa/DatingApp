using DatingApp.Data;
using DatingApp.Data.Models;
using DatingApp.DTOs;
using DatingApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class AccountController:ControllerBase
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
            if (await checkUserExist(registerDto.Username)) return BadRequest("user is found");

            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName=registerDto.Username.ToLower(),
                hashedPass=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                saltPassword=hmac.Key

            };
         
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = user.UserName,
                Token =_JwtToken.CreateToken(user)
            };
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> login(LoginDTO loginDto)
        {
            User user1=await _context.users.SingleOrDefaultAsync(x=>x.UserName==loginDto.userName);
            if (user1 == null) return Unauthorized("invaild email");

            using var hmac=new HMACSHA512(user1.saltPassword);

            var computedhash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for (int i = 0; i < computedhash.Length; i++)
            {
                if (computedhash[i] != user1.hashedPass[i]) return Unauthorized("invaild password");
            }

            return new UserDto
            {
                UserName = user1.UserName,
                Token = _JwtToken.CreateToken(user1)
            };
        }


        private async Task<bool> checkUserExist(string userName)
        {
            return await _context.users.AnyAsync(x => x.UserName == userName.ToLower());
        }
    }
}

