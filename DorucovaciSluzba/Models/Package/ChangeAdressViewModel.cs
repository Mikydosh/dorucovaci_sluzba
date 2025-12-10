using DorucovaciSluzba.Validations;
using System.ComponentModel.DataAnnotations;

namespace DorucovaciSluzba.Models.Package
{
    public class ChangeAddressViewModel
    {
        public int ZasilkaId { get; set; }
        public string Cislo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ulice je povinná")]
        [FirstLetterCapitalizedCZ]
        [Display(Name = "Ulice")]
        public string DestinaceUlice { get; set; } = string.Empty;

        [Required(ErrorMessage = "Číslo popisné je povinné")]
        [CpCZ]
        [Display(Name = "Číslo popisné")]
        public string DestinaceCP { get; set; } = string.Empty;

        [Required(ErrorMessage = "Město je povinné")]
        [FirstLetterCapitalizedCZ]
        [Display(Name = "Město")]
        public string DestinaceMesto { get; set; } = string.Empty;

        [Required(ErrorMessage = "PSČ je povinné")]
        [PscCZ]
        [Display(Name = "PSČ")]
        public string DestinacePsc { get; set; } = string.Empty;

        // Pro zobrazení
        public string PrijemceJmeno { get; set; } = string.Empty;
        public DateTime DatumOdeslani { get; set; }
    }
}