using System.ComponentModel;

namespace Business.Models
{
    public class SepetElemanModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }
        [DisplayName("�r�n Ad�")]
        public string UrunAdi { get; set; }
        [DisplayName("Birim Fiyat�")]
        public double BirimFiyati { get; set; }
        
    }
}
