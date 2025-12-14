using System.ComponentModel.DataAnnotations;
using DorucovaciSluzba.Validations;

namespace DorucovaciSluzba.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }

        [FirstLetterCapitalizedCZ]
        public string? FirstName { get; set; }

        [FirstLetterCapitalizedCZ]
        public string? LastName { get; set; }

        [Required]
        [EmailCZ]
        public string? Email { get; set; }

        [PhoneCZ]
        public string? Phone { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Hesla se neshodují!")]
        public string? RepeatedPassword { get; set; }
    }
}
