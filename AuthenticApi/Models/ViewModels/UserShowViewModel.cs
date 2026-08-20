namespace AuthenticApi.Models.ViewModels
{
    public class UserShowViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }
    }
}