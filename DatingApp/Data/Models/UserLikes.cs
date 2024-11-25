namespace DatingApp.Data.Models
{
    public class UserLikes
    {
        public int SourceUserId { get; set; }

        public User SourceUser { get; set; }

        public int TragetUserId { get; set; }

        public User TragerUser { get; set; }
    }
}
