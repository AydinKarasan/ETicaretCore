using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciKayitModel
    {
        [Required]
        [StringLength(15)]
        public string KullaniciAdi { get; set; }
        [Required]
        [StringLength(10)]
        public string Sifre { get; set; }
        [Required]
        [StringLength(10)]
        [Compare("Sifre",ErrorMessage = "Þifre ile þifre onay ayný olmalýdýr")]
        public string SifreOnay { get; set; }
        public KullaniciDetayiModel KullaniciDetayi { get; set; }
    }
}
