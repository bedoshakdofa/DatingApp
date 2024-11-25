﻿using System.ComponentModel.DataAnnotations;
namespace DatingApp.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] hashedPass { get; set; }

        public byte[] saltPassword { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string Gender { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Intrests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public List<Photo> Photos { get; set; } = new();

        public List<UserLikes> LikedUsers { get; set; }

        public List<UserLikes> LikedByUsers { get; set; }
    }
}
