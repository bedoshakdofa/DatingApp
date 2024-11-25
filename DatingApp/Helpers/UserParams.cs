namespace DatingApp.Helpers
{
    public class UserParams
    {
        private const int _MaxPageSize = 50;

        private int _PageSize = 10;

        public int pageNumber {  get; set; }  = 1;

        public int pageSize { 
            get => _PageSize;
            set => _PageSize=(value>_MaxPageSize)? _MaxPageSize:value;
        }

        public string Username { get; set; }

        public string Gender { get; set; }

        public string OrderBy { get; set; } = "LastActive";
    }
}
