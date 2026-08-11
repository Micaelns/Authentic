using System.ComponentModel.DataAnnotations;

namespace Authentic_Api.Models.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O apelido é obrigatório.")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; }
        public string PasswordRe { get; set; }

        public bool IsBlocked { get; set; }
    }
}