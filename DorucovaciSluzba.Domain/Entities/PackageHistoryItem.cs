using System.ComponentModel.DataAnnotations.Schema;


namespace DorucovaciSluzba.Domain.Entities
{
    [Table(nameof(PackageHistoryItem))]
    public class PackageHistoryItem : Entity<int>
    {
        [ForeignKey(nameof(Zasilka))]
        public int ZasilkaId { get; set; }

        [ForeignKey(nameof(Stav))]
        public int StavId { get; set; }

        public DateTime DatumZmeny { get; set; }

        // Navigační property
        public StavZasilka? Stav { get; set; }
    }
}
