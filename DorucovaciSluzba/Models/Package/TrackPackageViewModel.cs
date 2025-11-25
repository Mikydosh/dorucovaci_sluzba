using DorucovaciSluzba.Validations;
using System.ComponentModel.DataAnnotations;

namespace DorucovaciSluzba.Models.Package
{
    public class TrackPackageViewModel
    {
        // ČÍSLO ZÁSILKY
        [Required(ErrorMessage = "Číslo zásilky je povinné")]
        [PackageNumCZ]
        [Display(Name = "Číslo zásilky")]
        public string CisloZasilky { get; set; } = string.Empty;

        // E-MAIL
        [Required(ErrorMessage = "E-mail je povinný")]
        [EmailCZ]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;
    }
}
