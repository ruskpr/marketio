using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class LoginDTO
    {
        [Required]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "password")]
        public string PasswordHash { get; set; }
    }
}
