using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DorucovaciSluzba.Domain.Entities
{
    [Table(nameof(TypUzivatel))]
    public class TypUzivatel : Entity<int>
    {
        public string Typ { get; set; } = "uzivatel";
    }
}
