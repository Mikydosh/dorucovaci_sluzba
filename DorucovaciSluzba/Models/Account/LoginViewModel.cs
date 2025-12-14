using System.ComponentModel.DataAnnotations;


namespace DorucovaciSluzba.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public bool LoginFailed { get; set; }
    }
}
