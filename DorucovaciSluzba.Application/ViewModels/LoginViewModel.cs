using System.ComponentModel.DataAnnotations;


namespace DorucovaciSluzba.Application.ViewModels
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
