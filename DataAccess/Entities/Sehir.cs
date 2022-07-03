using AppCorev1.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Sehir : RecordBase
    {
        [Required]
        [StringLength(250)]
        public string Adi { get; set; }
        public int UlkeId { get; set; }
        public Ulke Ulke { get; set; }
        public List<KullaniciDetayi> KullaniciDetaylari { get; set; }
    }
}
