using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorucovaciSluzba.Domain.Entities
{
    [Table(nameof(Zasilka))]
    public class Zasilka : Entity<int>
    {
        public string Cislo { get; set; } = string.Empty;

        public DateTime DatumOdeslani { get; set; }

        [ForeignKey(nameof(Stav))]
        public int StavId { get; set; }
        public StavZasilka? Stav { get; set; }

        [Required]
        public string DestinaceUlice { get; set; } = string.Empty;

        [Required]
        public string DestinaceCP { get; set; } = string.Empty;

        [Required]
        public string DestinaceMesto { get; set; } = string.Empty;

        [Required]
        public string DestinacePsc { get; set; } = string.Empty;

        [ForeignKey(nameof(Odesilatel))]
        public int OdesilatelId { get; set; }
        public Uzivatel? Odesilatel { get; set; }

        [ForeignKey(nameof(Prijemce))]
        public int PrijemceId { get; set; }
        public Uzivatel? Prijemce { get; set; }

        [ForeignKey(nameof(Kuryr))]
        public int? KuryrId { get; set; }
        public Uzivatel? Kuryr { get; set; }
    }
}
