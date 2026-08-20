namespace Authentic_Api.Models.ViewModels
{
    public class RoleCheckViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}