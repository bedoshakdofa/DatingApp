using DatingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApp.Data
{
    public class Seed
    {
        public static async Task GetSeed(DbContextApplication context)
        {
            if (await context.users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var option=new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

            var Users=JsonSerializer.Deserialize<List<User>>(userData);

            foreach (var user in Users) {

                using var hmac = new HMACSHA512();
                user.UserName=user.UserName.ToLower();
                user.hashedPass=hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.saltPassword=hmac.Key;
                context.users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
