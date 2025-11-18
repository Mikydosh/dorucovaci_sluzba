using DorucovaciSluzba.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DorucovaciSluzba.Models.Package
{
    public class EditZasilkaViewModel
    {
        public int Id { get; set; }

        // POUZE ZOBRAZENÍ
        [Display(Name = "Číslo zásilky")]
        public string Cislo { get; set; } = string.Empty;

        [Display(Name = "Datum odeslání")]
        public DateTime DatumOdeslani { get; set; }

        [Display(Name = "Odesílatel")]
        public string OdesilatelJmeno { get; set; } = string.Empty;

        [Display(Name = "Příjemce")]
        public string PrijemceJmeno { get; set; } = string.Empty;

        public string DestinaceAdresa { get; set; } = string.Empty;

        // EDITOVATELNÉ POLE: STAV
        [Display(Name = "Stav zásilky")]
        public int StavId { get; set; }

        // EDITOVATELNÉ POLE: KURÝR
        [Display(Name = "Přiřazený kurýr")]
        public int? KuryrId { get; set; }

        // Pro dropdowny
        public List<StavZasilka>? DostupneStavy { get; set; }
        public List<Uzivatel>? DostupniKuryri { get; set; }
    }
}
