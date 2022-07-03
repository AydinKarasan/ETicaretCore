using System.ComponentModel;

namespace Business.Models
{
    public class SepetElemanModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }
        [DisplayName("Ürün Adý")]
        public string UrunAdi { get; set; }
        [DisplayName("Birim Fiyatý")]
        public double BirimFiyati { get; set; }
        
    }
}
