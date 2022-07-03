using System.ComponentModel;

namespace Business.Models
{
    public class SepetElemanGroupByModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }
        [DisplayName("Ürün Adý")]
        public string UrunAdi { get; set; }
        public double ToplamUrunBirimFiyati { get; set; }
        [DisplayName("Toplam Ürün Fiyatý")]
        public string ToplamUrunBirimFiyatiDisplay { get; set; }
        [DisplayName("Toplam Ürün Adedi")]
        public int ToplamUrunAdedi { get; set; }
    }
}
