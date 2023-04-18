using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100), MinLength(3, ErrorMessage = "Email must be more the 2 characters.")]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "First name must be less than 50 characters.")]
        [Display(Name = "first name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name must be less than 50 characters.")]
        [Display(Name = "last name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "password")]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(64)]
        [Display(Name = "confirm password")]
        [Compare("PasswordHash",
        ErrorMessage = "password fields do not match.")]
        public string ConfirmPasswordHash { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

    }
}
