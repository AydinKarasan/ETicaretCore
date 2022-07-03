using System.ComponentModel;

namespace Business.Models
{
    public class SepetElemanGroupByModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }
        [DisplayName("�r�n Ad�")]
        public string UrunAdi { get; set; }
        public double ToplamUrunBirimFiyati { get; set; }
        [DisplayName("Toplam �r�n Fiyat�")]
        public string ToplamUrunBirimFiyatiDisplay { get; set; }
        [DisplayName("Toplam �r�n Adedi")]
        public int ToplamUrunAdedi { get; set; }
    }
}
