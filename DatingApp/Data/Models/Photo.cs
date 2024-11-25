using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Data.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool isMain { get; set; }

        public string publicId { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }
    }
}