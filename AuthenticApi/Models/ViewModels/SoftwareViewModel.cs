using System.ComponentModel.DataAnnotations;

namespace Authentic_Api.Models.ViewModels
{
    public class SoftwareViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}