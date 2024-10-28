using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
