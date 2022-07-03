using AppCorev1.Records.Bases;

using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KullaniciModel : RecordBase
    {
        [Required]
        [StringLength(15)]
        public string KullaniciAdi { get; set; }
        [Required]
        [StringLength(10)]
        public string Sifre { get; set; }
        public bool AktifMi { get; set; }        
        public int RolId { get; set; }
        
        public string RolAdiDisplay { get; set; }
        public KullaniciDetayiModel KullaniciDetayi { get; set; }
    }
}
