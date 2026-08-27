namespace AuthenticApi.DTOs.Auth
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int SoftwareId { get; set; } = 0; 
    }
}