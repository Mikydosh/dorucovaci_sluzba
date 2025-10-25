using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DorucovaciSluzba.Domain.Entities
{
    [Table(nameof(StavZasilka))]
    public class StavZasilka : Entity<int>
    {
        public string Stav { get; set; } = "Objednávka vytvořena";
    }
}
