using System.ComponentModel.DataAnnotations;

namespace Lumia.ViewModels.User
{
    public class RegisterUserVM
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public string Surname{ get; set; }
        [Required]
        public string Username{ get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
