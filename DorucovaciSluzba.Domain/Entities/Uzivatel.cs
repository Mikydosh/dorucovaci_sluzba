using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorucovaciSluzba.Domain.Entities
{
    [Table(nameof(Uzivatel))]
    public class Uzivatel : Entity<int>
    {
        [Required]
        public string Jmeno { get; set; } = string.Empty;

        [Required]
        public string Prijmeni { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Heslo { get; set; } = string.Empty;
        public DateTime? DatumNarozeni { get; set; }
        public string? Telefon { get; set; }
        public string? Ulice { get; set; }
        public string? Mesto { get; set; }
        public string? Psc { get; set; }

        [ForeignKey(nameof(Typ))]
        public int TypId { get; set; }
        public TypUzivatel? Typ { get; set; }

        public ICollection<Zasilka> OdeslaneZasilky { get; set; } = new List<Zasilka>();
        public ICollection<Zasilka> PrijateZasilky { get; set; } = new List<Zasilka>();
    }
}
