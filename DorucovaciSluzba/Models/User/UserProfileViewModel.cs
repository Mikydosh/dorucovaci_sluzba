using System.ComponentModel.DataAnnotations;
using DorucovaciSluzba.Validations;

namespace DorucovaciSluzba.Models.User
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jméno je povinné")]
        [FirstLetterCapitalizedCZ]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení je povinné")]
        [FirstLetterCapitalizedCZ]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mailová adresa je povinná")]
        [EmailCZ]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [PhoneCZ]
        [Display(Name = "Telefon")]
        public string? Telefon { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        public DateTime? DatumNarozeni { get; set; }

        // ADRESA
        [FirstLetterCapitalizedCZ]
        [Display(Name = "Ulice")]
        public string? Ulice { get; set; }

        [CpCZ]
        [Display(Name = "Číslo popisné")]
        public string? CP { get; set; }

        [FirstLetterCapitalizedCZ]
        [Display(Name = "Město")]
        public string? Mesto { get; set; }

        [PscCZ]
        [Display(Name = "PSČ")]
        public string? Psc { get; set; }

        // ZMĚNA HESLA (volitelné)
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potvrdit nové heslo")]
        [Compare(nameof(NewPassword), ErrorMessage = "Hesla se neshodují!")]
        public string? ConfirmPassword { get; set; }
    }
}
