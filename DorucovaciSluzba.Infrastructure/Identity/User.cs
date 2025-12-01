using Microsoft.AspNetCore.Identity;
using DorucovaciSluzba.Domain.Entities;

namespace DorucovaciSluzba.Infrastructure.Identity
{
    public class User : IdentityUser<int>
    {   
        // Data
        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }

        public virtual DateTime? DatumNarozeni { get; set; }
        public virtual string? Telefon { get; set; }
        public virtual string? Ulice { get; set; }
        public virtual string? CP { get; set; }
        public virtual string? Mesto { get; set; }
        public virtual string? Psc { get; set; }

        // Vztahy k zásilkám
        public virtual ICollection<Zasilka> OdeslaneZasilky { get; set; } = new List<Zasilka>();
        public virtual ICollection<Zasilka> PrijateZasilky { get; set; } = new List<Zasilka>();
        public virtual ICollection<Zasilka> DorucovaciZasilky { get; set; } = new List<Zasilka>();
    }
}
