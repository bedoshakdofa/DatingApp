using System.ComponentModel.DataAnnotations;

namespace DatingApp.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string userName { get; set; }

        public string User_email { get; set; }

        public byte[] hashedPass{ get; set; }

        public byte[] saltPassword { get; set; }
    }
}
