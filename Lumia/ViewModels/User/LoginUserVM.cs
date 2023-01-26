using System.ComponentModel.DataAnnotations;

namespace Lumia.ViewModels
{
    public class LoginUserVM
    {
        [Required]
        public string UserNameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
