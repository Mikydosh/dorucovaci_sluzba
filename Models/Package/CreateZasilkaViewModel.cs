using System.ComponentModel.DataAnnotations;

namespace DorucovaciSluzba.Models.Package
{
    public class CreateZasilkaViewModel
    {
        // ODESÍLATEL
        [Required(ErrorMessage = "Jméno odesílatele je povinné")]
        [Display(Name = "Jméno")]
        public string OdesilatelJmeno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení odesílatele je povinné")]
        [Display(Name = "Příjmení")]
        public string OdesilatelPrijmeni { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail odesílatele je povinný")]
        [Display(Name = "E-mail")]
        public string OdesilatelEmail { get; set; } = string.Empty;

        // ADRESA ODESÍLATELE
        [Required(ErrorMessage = "Ulice odesílatele je povinná")]
        [Display(Name = "Ulice")]
        public string OdesilatelUlice { get; set; } = string.Empty;

        [Required(ErrorMessage = "Číslo popisné je povinné")]
        [Display(Name = "Číslo popisné")]
        public string OdesilatelCP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Město odesílatele je povinné")]
        [Display(Name = "Město")]
        public string OdesilatelMesto { get; set; } = string.Empty;

        [Required(ErrorMessage = "PSČ odesílatele je povinné")]
        [Display(Name = "PSČ")]
        public string OdesilatelPsc { get; set; } = string.Empty;

        // PŘÍJEMCE
        [Required(ErrorMessage = "Jméno příjemce je povinné")]
        [Display(Name = "Jméno")]
        public string PrijemceJmeno { get; set; } = string.Empty;

        [Required(ErrorMessage = "Příjmení příjemce je povinné")]
        [Display(Name = "Příjmení")]
        public string PrijemcePrijmeni { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail příjemce je povinný")]
        [Display(Name = "E-mail")]
        public string PrijemceEmail { get; set; } = string.Empty;

        // ADRESA PŘÍJEMCE
        [Required(ErrorMessage = "Ulice je povinná")]
        [Display(Name = "Ulice")]
        public string DestinaceUlice { get; set; } = string.Empty;

        [Required(ErrorMessage = "Číslo popisné je povinné")]
        [Display(Name = "Číslo popisné")]
        public string DestinaceCP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Město je povinné")]
        [Display(Name = "Město")]
        public string DestinaceMesto { get; set; } = string.Empty;

        [Required(ErrorMessage = "PSČ je povinné")]
        [Display(Name = "PSČ")]
        public string DestinacePsc { get; set; } = string.Empty;
    }
}
