using System.ComponentModel.DataAnnotations;

namespace MarketIO.Shared.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100), MinLength(3)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(64)]
        public string ConfirmPasswordHash { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

    }
}
